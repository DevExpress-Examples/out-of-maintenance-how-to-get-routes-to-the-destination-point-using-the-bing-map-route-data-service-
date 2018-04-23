Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Map

Namespace CalculateRoutesFromMajorRoads
    Partial Public Class MainPage
        Inherits UserControl

        Private receiveLocationLocation As New GeoPoint(40, -120)
        Public Property ReceiveLocation() As GeoPoint
            Get
                Return receiveLocationLocation
            End Get
            Set(ByVal value As GeoPoint)
                receiveLocationLocation = value
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
            Me.DataContext = ReceiveLocation
        End Sub

        #Region "#CalculateRoutesFromMajorRoads"
        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            routeProvider.CalculateRoutesFromMajorRoads(New RouteWaypoint("Destination Point", receiveLocationLocation))
        End Sub
        #End Region ' #CalculateRoutesFromMajorRoads

        #Region "#RouteCalculated"
        Private Sub routeProvider_RouteCalculated(ByVal sender As Object, ByVal e As BingRouteCalculatedEventArgs)
            Dim result As RouteCalculationResult = e.CalculationResult

            Dim resultList As New StringBuilder()
            resultList.Append(String.Format("Status: {0}" & ControlChars.Lf, result.ResultCode))
            resultList.Append(String.Format("Fault reason: {0}" & ControlChars.Lf, result.FaultReason))
            resultList.Append(ProcessStartingPoints(result.StartingPoints))
            resultList.Append(ProcessRouteResults(result.RouteResults))

            tbResult.Text = resultList.ToString()
        End Sub
        #End Region ' #RouteCalculated

        #Region "#ProcessStartingPoints"
        Private Function ProcessStartingPoints(ByVal points As List(Of RouteWaypoint)) As String
            If points Is Nothing Then
                Return ""
            End If

            Dim sb As New StringBuilder("Starting Points:" & ControlChars.Lf)
            sb.Append(String.Format("_________________________" & ControlChars.Lf))
            For i As Integer = 0 To points.Count - 1
                sb.Append(String.Format("Starting point {0}: {1} ({2})" & ControlChars.Lf, i + 1, points(i).Description, points(i).Location))
            Next i
            Return sb.ToString()
        End Function
        #End Region ' #ProcessStartingPoints

        #Region "#ProcessRouteResults"
        Private Function ProcessRouteResults(ByVal results As List(Of BingRouteResult)) As String
            If results Is Nothing Then
                Return ""
            End If

            Dim sb As New StringBuilder("RouteResults:" & ControlChars.Lf)
            For i As Integer = 0 To results.Count - 1
                sb.Append(String.Format("_________________________" & ControlChars.Lf))
                sb.Append(String.Format("Path {0}:" & ControlChars.Lf, i + 1))
                sb.Append(String.Format("Distance: {0}" & ControlChars.Lf, results(i).Distance))
                sb.Append(String.Format("Time: {0}" & ControlChars.Lf, results(i).Time))
                sb.Append(ProcessLegs(results(i).Legs))
            Next i
            Return sb.ToString()
        End Function
        #End Region ' #ProcessRouteResults

        #Region "#ProcessLegs"
        Private Function ProcessLegs(ByVal legs As List(Of BingRouteLeg)) As String
            If legs Is Nothing Then
                Return ""
            End If

            Dim sb As New StringBuilder("Legs:" & ControlChars.Lf)
            For i As Integer = 0 To legs.Count - 1
                sb.Append(String.Format(ControlChars.Tab & "Leg {0}:" & ControlChars.Lf, i + 1))
                sb.Append(String.Format(ControlChars.Tab & "Start: {0}" & ControlChars.Lf, legs(i).StartPoint))
                sb.Append(String.Format(ControlChars.Tab & "End: {0}" & ControlChars.Lf, legs(i).EndPoint))
                sb.Append(String.Format(ControlChars.Tab & "Distance: {0}" & ControlChars.Lf, legs(i).Distance))
                sb.Append(String.Format(ControlChars.Tab & "Time: {0}" & ControlChars.Lf, legs(i).Time))
                sb.Append(ProcessItinerary(legs(i).Itinerary))
            Next i
            Return sb.ToString()
        End Function
        #End Region ' #ProcessLegs

        #Region "#ProcessItinerary"
        Private Function ProcessItinerary(ByVal items As List(Of BingItineraryItem)) As String
            If items Is Nothing Then
                Return ""
            End If

            Dim sb As New StringBuilder(ControlChars.Tab & "Internary Items:" & ControlChars.Lf)
            For i As Integer = 0 To items.Count - 1
                sb.Append(String.Format(ControlChars.Tab & ControlChars.Tab & "Itinerary {0}:" & ControlChars.Lf, i + 1))
                sb.Append(String.Format(ControlChars.Tab & ControlChars.Tab & "Maneuver: {0}" & ControlChars.Lf, items(i).Maneuver))
                sb.Append(String.Format(ControlChars.Tab & ControlChars.Tab & "Location: {0}" & ControlChars.Lf, items(i).Location))
                sb.Append(String.Format(ControlChars.Tab & ControlChars.Tab & "Instructions: {0}" & ControlChars.Lf, items(i).ManeuverInstruction))
                sb.Append(ProcessWarnings(items(i).Warnings))
            Next i
            Return sb.ToString()
        End Function
        #End Region ' #ProcessItinerary

        #Region "#ProcessWarnings"
        Private Function ProcessWarnings(ByVal warnings As List(Of BingItineraryItemWarning)) As String
            If warnings Is Nothing Then
                Return ""
            End If

            Dim sb As New StringBuilder(ControlChars.Tab & ControlChars.Tab & "Warnings:" & ControlChars.Lf)
            For i As Integer = 0 To warnings.Count - 1
                sb.Append(String.Format(ControlChars.Tab & ControlChars.Tab & ControlChars.Tab & "Warning {0}:" & ControlChars.Lf, i + 1))
                sb.Append(String.Format(ControlChars.Tab & ControlChars.Tab & ControlChars.Tab & "Type: {0}" & ControlChars.Lf, warnings(i).Type))
                sb.Append(String.Format(ControlChars.Tab & ControlChars.Tab & ControlChars.Tab & "Text: {0}" & ControlChars.Lf, warnings(i).Text))

            Next i
            Return sb.ToString()
        End Function
        #End Region ' #ProcessWarnings
    End Class
End Namespace
