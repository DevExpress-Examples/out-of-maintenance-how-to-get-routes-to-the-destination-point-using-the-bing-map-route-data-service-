<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128570921/14.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4269)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* **[MainPage.xaml](./CS/XpfMapFromMajorRoutes/MainPage.xaml) (VB: [MainPage.xaml](./VB/XpfMapFromMajorRoutes/MainPage.xaml))**
* [MainPage.xaml.cs](./CS/XpfMapFromMajorRoutes/MainPage.xaml.cs) (VB: [MainPage.xaml.vb](./VB/XpfMapFromMajorRoutes/MainPage.xaml.vb))
<!-- default file list end -->
# How to get routes to the destination point using the Bing Map Route Data Service  


<p>This example demonstrates how to calculate routes to the destination point from major roads using the <a href="http://documentation.devexpress.com/#Silverlight/DevExpressXpfMapBingRouteDataProvider_CalculateRoutesFromMajorRoadstopic"><u>BingRouteDataProvider.CalculateRoutesFromMajorRoads</u></a> method.</p>
<p>Before route calculation, specify destination point coordinates (<a href="http://documentation.devexpress.com/#Silverlight/DevExpressXpfMapGeoPoint_Latitudetopic"><u>GeoPoint.Latitude</u></a> and <a href="http://documentation.devexpress.com/#Silverlight/DevExpressXpfMapGeoPoint_Longitudetopic"><u>GeoPoint.Longitude</u></a>). In addition, you can specify the optional parameters: the destination name (<a href="http://documentation.devexpress.com/#Silverlight/DevExpressXpfMapRouteWaypoint_Descriptiontopic"><u>RouteWaypoint.Description</u></a>), driving or walking route travel mode using the <a href="http://documentation.devexpress.com/#Silverlight/DevExpressXpfMapBingRouteOptions_Modetopic"><u>BingRouteOptions.Mode</u></a> property and route optimization options to calculate the optimal route either by time or by distance via the <a href="http://documentation.devexpress.com/#Silverlight/DevExpressXpfMapBingRouteOptions_RouteOptimizationtopic"><u>BingRouteOptions.RouteOptimization</u></a> property.</p>
<p>To start the application, click the <strong>Calculate Routes from Major Roads</strong> button, it handles the <strong>calculateRoutes_Click </strong>event. All parameters are passed to the <strong>CalculateMajorRoutes </strong>method, and you can see the results in the textblock element below and calculated routes on a map. </p>
<p>The requested results contain the total distance of a route, route leg, itinerary item (<a href="http://documentation.devexpress.com/#Silverlight/DevExpressXpfMapBingRouteResult_Distancetopic"><u>BingRouteResult.Distance</u></a>, <a href="http://documentation.devexpress.com/#Silverlight/DevExpressXpfMapBingRouteLeg_Distancetopic"><u>BingRouteLeg.Distance</u></a>, <a href="http://documentation.devexpress.com/#Silverlight/DevExpressXpfMapBingItineraryItem_Distancetopic"><u>BingItineraryItem.Distance</u></a>), the time required to follow the calculated route (<a href="http://documentation.devexpress.com/#Silverlight/DevExpressXpfMapBingRouteResult_Timetopic"><u>BingRouteResult.Time</u></a>) and pass the rout leg and itinerary item (<a href="http://documentation.devexpress.com/#Silverlight/DevExpressXpfMapBingRouteLeg_Timetopic"><u>BingRouteLeg.Time</u></a>, <a href="http://documentation.devexpress.com/#Silverlight/DevExpressXpfMapBingItineraryItem_Timetopic"><u>BingItineraryItem.Time</u></a>). You can also see the maneuver instructions associated with the itinerary item (<a href="http://documentation.devexpress.com/#Silverlight/DevExpressXpfMapBingItineraryItem_ManeuverInstructiontopic"><u>BingItineraryItem.ManeuverInstruction</u></a>) and other parameters.</p>
<p>Note that if you run this sample as is, you will get a warning message informing that the specified Bing Maps key is invalid. To learn more about Bing Map keys, please refer to the <a href="http://documentation.devexpress.com/#Silverlight/CustomDocument5975"><u>How to: Get a Bing Maps Key</u></a> tutorial.</p>

<br/>


