Imports Microsoft.VisualBasic
Imports System.Windows.Controls
Imports DevExpress.Xpf.Map
Imports System
Imports System.Text
Imports System.Windows
Imports System.Windows.Media

Namespace CalculateRoutes
	Partial Public Class MainPage
		Inherits UserControl
		Private latitude As Double
		Private longitude As Double
		Private destination As String
		Private options As BingRouteOptions

		Public Sub New()
			InitializeComponent()
			Me.options = New BingRouteOptions()
		End Sub

		Private Sub calculateRoutes_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			If GetCalculateRoutesArguments() Then
				CalculateMajorRoutes()
			End If
		End Sub

		Private Function GetCalculateRoutesArguments() As Boolean
			latitude = If(String.IsNullOrEmpty(tbLatitude.Text), 0, Double.Parse(tbLatitude.Text))
			If (latitude > 90) OrElse (latitude < -90) Then
				MessageBox.Show("Latitude must be less than or equal to 90 and greater than or equal to - 90. Please, correct the input value.")
				Return False
			End If

			longitude = If(String.IsNullOrEmpty(tbLongitude.Text), 0, Double.Parse(tbLongitude.Text))
			If (longitude > 180) OrElse (longitude < -180) Then
				MessageBox.Show("Longitude must be less than or equal to 180 and greater than or equal to - 180. Please, correct the input value.")
				Return False
			End If

			destination = tbDestination.Text

			If cbMode.SelectedIndex = 0 Then
				options.Mode = BingTravelMode.Driving
			Else
				options.Mode = BingTravelMode.Walking
			End If

			If cbOptimize.SelectedIndex = 0 Then
				options.RouteOptimization = BingRouteOptimization.MinimizeTime
			Else
				options.RouteOptimization = BingRouteOptimization.MinimizeDistance
			End If

			Return True
		End Function

		Private Sub CalculateMajorRoutes()
			CalculateMajorRouteRequest(destination, New GeoPoint(latitude, longitude), options)
		End Sub

		Private Sub CalculateMajorRouteRequest(ByVal destination As String, ByVal location As GeoPoint, ByVal options As BingRouteOptions)
			Try
'				#Region "#CalculateMajorRoutes"
				routeProvider.RouteOptions = options
				routeProvider.CalculateRoutesFromMajorRoads(New RouteWaypoint(destination, location), 2.0)
'				#End Region ' #CalculateMajorRoutes
			Catch ex As Exception
				MessageBox.Show("An error occurs: " & ex.ToString())
			End Try
		End Sub

		Private Sub routeDataProvider_RouteCalculated(ByVal sender As Object, ByVal e As BingRouteCalculatedEventArgs)
			mapControl1.CenterPoint = New GeoPoint(latitude, longitude)
			mapControl1.ZoomLevel = 10
			Dim result As RouteCalculationResult = e.CalculationResult
			Dim resultList As New StringBuilder("")
			resultList.Append(String.Format("Status: {0}" & Constants.vbLf, result.ResultCode))
			resultList.Append(String.Format("Fault reason: {0}" & Constants.vbLf, result.FaultReason))

			If result.StartingPoints IsNot Nothing Then
				resultList.Append(String.Format("_________________________" & Constants.vbLf))
				Dim i As Integer = 1
				For Each startingPoint As RouteWaypoint In result.StartingPoints
					resultList.Append(String.Format("Starting point {0}: {1} ({2})" & Constants.vbLf, i, startingPoint.Description, startingPoint.Location))
				Next startingPoint
					i += 1
			End If

			If result.RouteResults IsNot Nothing Then
				Dim rnum As Integer = 1
				For Each routeResult As BingRouteResult In result.RouteResults
					resultList.Append(String.Format("_________________________" & Constants.vbLf))
					resultList.Append(String.Format("Path {0}:" & Constants.vbLf, rnum))
					rnum += 1
					resultList.Append(String.Format("Distance: {0}" & Constants.vbLf, routeResult.Distance))
					resultList.Append(String.Format("Time: {0}" & Constants.vbLf, routeResult.Time))

					Dim path As New MapPolyline()
					path.Stroke = New SolidColorBrush(Colors.Red)
					path.StrokeStyle = New StrokeStyle() With {.Thickness = 2}
					For Each point As GeoPoint In routeResult.RoutePath
						path.Points.Add(point)
					Next point

					If routeResult.Legs IsNot Nothing Then
						Dim legNum As Integer = 1
						For Each leg As BingRouteLeg In routeResult.Legs
							resultList.Append(String.Format(Constants.vbTab & "Leg {0}:" & Constants.vbLf, legNum))
							legNum += 1
							resultList.Append(String.Format(Constants.vbTab & "Start: {0}" & Constants.vbLf, leg.StartPoint))
							resultList.Append(String.Format(Constants.vbTab & "End: {0}" & Constants.vbLf, leg.EndPoint))
							resultList.Append(String.Format(Constants.vbTab & "Distance: {0}" & Constants.vbLf, leg.Distance))
							resultList.Append(String.Format(Constants.vbTab & "Time: {0}" & Constants.vbLf, leg.Time))
							If leg.Itinerary IsNot Nothing Then
								Dim itNum As Integer = 1
								For Each itineraryItem As BingItineraryItem In leg.Itinerary
									resultList.Append(String.Format(Constants.vbTab + Constants.vbTab & "Itinerary {0}:" & Constants.vbLf, itNum))
									itNum += 1
									resultList.Append(String.Format(Constants.vbTab + Constants.vbTab & "Maneuver: {0}" & Constants.vbLf, itineraryItem.Maneuver))
									resultList.Append(String.Format(Constants.vbTab + Constants.vbTab & "Location: {0}" & Constants.vbLf, itineraryItem.Location))
									resultList.Append(String.Format(Constants.vbTab + Constants.vbTab & "Instructions: {0}" & Constants.vbLf, itineraryItem.ManeuverInstruction))
									Dim warnNum As Integer = 1
									For Each warning As BingItineraryItemWarning In itineraryItem.Warnings
										resultList.Append(String.Format(Constants.vbTab + Constants.vbTab + Constants.vbTab & "Warning {0}:" & Constants.vbLf, warnNum))
										warnNum += 1
										resultList.Append(String.Format(Constants.vbTab + Constants.vbTab + Constants.vbTab & "Type: {0}" & Constants.vbLf, warning.Type))
										resultList.Append(String.Format(Constants.vbTab + Constants.vbTab + Constants.vbTab & "Text: {0}" & Constants.vbLf, warning.Text))
									Next warning
								Next itineraryItem
							End If
                            If Not leg.StartPoint = Nothing Then
                                Dim start As New MapDot()
                                start.Size = 10
                                start.Location = leg.StartPoint

                            End If
                            If Not leg.EndPoint = Nothing Then
                                Dim [end] As New MapDot()
                                [end].Size = 15
                                [end].Location = leg.EndPoint

                            End If
						Next leg
					End If
				Next routeResult
			End If

			tbResults.Text = resultList.ToString()
		End Sub

	End Class
End Namespace
