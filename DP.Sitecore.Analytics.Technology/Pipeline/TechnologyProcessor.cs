using System;
using System.Collections.Generic;
using System.Linq;
using DP.Sitecore.Analytics.Technology.Model.Dimensions;
using DP.Sitecore.Analytics.Technology.Model.Facts;
using Sitecore.Analytics.Aggregation.Pipeline;
using Sitecore.Analytics.Core;
using Sitecore.Analytics.Model;
using Sitecore.Diagnostics;
using Sitecore.ExperienceAnalytics.Aggregation;
using Sitecore.ExperienceAnalytics.Aggregation.Data.Schema;
using Sitecore.ExperienceAnalytics.Aggregation.Dimensions;
using Sitecore.ExperienceAnalytics.Core.Repositories.Contracts;
using Sitecore.ExperienceAnalytics.Core.Repositories.Model;

namespace DP.Sitecore.Analytics.Technology.Pipeline
{
    public class TechnologyProcessor : AggregationProcessor
    {
        private static ISegmentDefinitionService segmentDefinitionService;
        private static List<Dimension> Dimensions;

        static TechnologyProcessor()
        {
            Dimensions = new List<Dimension>();
            Dimensions.Add(new Dimension(new Guid("{5E4B8313-6D42-47E4-A2B6-CCF58CD01DD2}"), DimensionType.Browser));
            Dimensions.Add(new Dimension(new Guid("{C6FB38A6-96EA-4B19-901C-12D8E0366ACE}"), DimensionType.OS));
            Dimensions.Add(new Dimension(new Guid("{EFD666F6-C0C8-4F40-BF7F-DF7396D075B8}"), DimensionType.Screen));
            Dimensions.Add(new Dimension(new Guid("{65B97666-6170-40AB-AF73-D407172C0D44}"), DimensionType.UserAgent));

            segmentDefinitionService = AggregationContainer.Repositories.GetSegmentDefinitionService();
        }

        protected override void OnProcess(AggregationPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            VisitData visit = args.Context.Visit;

            if (visit.Browser == null || visit.OperatingSystem == null || visit.Screen == null || string.IsNullOrEmpty(visit.UserAgent))
            {
                return;
            }            

            Hash32 languageHash = AggregationProcessor.UpdateLanguagesDimension(args);
            Hash32 sitesHash = AggregationProcessor.UpdateSiteNamesDimension(args);

            InteractionData interactionData = new InteractionData(args, visit);

            try
            {
                ProcessDimensionSegments(args, visit, sitesHash, interactionData);
            }
            catch { }
        }

        private static void ProcessDimensionSegments(AggregationPipelineArgs args, VisitData visit, Hash32 sitesHash, InteractionData interactionData)
        {
            foreach(Dimension dimension in Dimensions)
            {
                IEnumerable<Segment> segments = from segment in segmentDefinitionService.GetSegmentDefinitions()
                                                where segment.DimensionId == dimension.DimensionID
                                                select new Segment(segment, dimension);

                foreach(Segment segment in segments)
                {
                    ISegmentKeyProvider keyProvider = null;
                    
                    switch(segment.Dimension.DimensionType)
                    {
                        case DimensionType.Browser:
                            keyProvider = interactionData.Browser;
                            break;
                        case DimensionType.OS:
                            keyProvider = interactionData.OS;
                            break;
                        case DimensionType.Screen:
                            keyProvider = interactionData.Screen;
                            break;
                        case DimensionType.UserAgent:
                            keyProvider = interactionData.UserAgent;
                            break;
                    }

                    string segmentKeyValue = keyProvider.GetSegmentKeyValue();
                    Hash64 segmentDimensionKeyId = args.GetDimension<DimensionKeys>().Add(segmentKeyValue);
                    
                    Hash64 segmentRecordId = args.GetDimension<SegmentRecords>().Add(segment.Definition.Id, visit.StartDateTime, sitesHash, segmentDimensionKeyId);

                    SegmentMetrics segmentMetricsFact = args.GetFact<SegmentMetrics>();
                    byte contactTransitionType = 1;
                    if (visit.ContactVisitIndex > 1) contactTransitionType = 2;

                    SegmentMetrics.Key segmentMetricsKey = new SegmentMetrics.Key
                    {
                        ContactTransitionType = contactTransitionType,
                        SegmentRecordId = segmentRecordId
                    };

                    List<PageEventData> evts = (from page in visit.Pages select page.PageEvents).FirstOrDefault();

                    SegmentMetricsValue segmentMetricsValue = new SegmentMetricsValue
                    {
                        //Same exact code from DimensionBase.CalculateCommonMetrics
                        Visits = 1,
                        Value = visit.Value,
                        Bounces = visit.Pages.Count == 1 ? 1 : 0,
                        Conversions = evts.Count<PageEventData>(e => e.IsGoal),
                        TimeOnSite = visit.Pages.Sum<PageData>((Func<PageData, int>)(page => DimensionBase.ConvertDuration(page.Duration))),
                        Pageviews = visit.Pages.Count,
                        Count = visit.VisitPageCount
                    };

                    segmentMetricsFact.Emit(segmentMetricsKey, segmentMetricsValue);
                }
            }           
        }

        private class InteractionData
        {
            public Browsers Browser { get; private set; }
            public OS OS { get; private set; }
            public Screens Screen { get; private set; }
            public UserAgents UserAgent { get; private set; }

            public InteractionData(AggregationPipelineArgs args, VisitData visit)
            {
                Browsers browserDimension = args.GetDimension<Browsers>();
                OS osDimension = args.GetDimension<OS>();
                Screens screenDimension = args.GetDimension<Screens>();
                UserAgents userAgentDimension = args.GetDimension<UserAgents>();

                Browser = browserDimension.Add(visit.Browser.BrowserMajorName, visit.Browser.BrowserMinorName, visit.Browser.BrowserVersion);                
                OS = osDimension.Add(visit.OperatingSystem.Name);
                Screen = screenDimension.Add(visit.Screen.ScreenHeight, visit.Screen.ScreenWidth);
                UserAgent = userAgentDimension.Add(visit.UserAgent);
            }
        }

        private class Dimension
        {
            public Guid DimensionID {get; private set; }
            public DimensionType DimensionType { get; private set; }

            public Dimension(Guid dimensionID, DimensionType dimensionType)
            {
                Assert.IsNotNull(dimensionID, "dimensionID");

                DimensionID = dimensionID;
                DimensionType = dimensionType;
            }
        }

        private class Segment
        {
            public SegmentDefinition Definition { get; private set; }
            public Dimension Dimension { get; private set; }

            public Segment(SegmentDefinition definition, Dimension dimension)
            {
                Definition = definition;
                Dimension = dimension;
            }
        }

        private enum DimensionType
        {
            Browser,
            OS,
            Screen,
            UserAgent,
            Account
        }
    }
}
