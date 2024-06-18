Public Class Cl_Orders
    Public Property id As String = ""
    Public Property corder_no As String = ""
    Public Property order_no As String = ""
    Public Property ol_code As Cl_SubID
    Public Property merch_code As String = ""
    Public Property merch_id As Integer = 0
    Public Property doc_type As Int16 = 2
    Public Property [date] As String
    Public Property dateto As String
    Public Property vatcalcmod As Integer = 1
    Public Property status As Int16 = 2
    Public Property orderLocalDetail As List(Of Cl_OrderLocalDetail)
    Public Property dtlm As String
    Public Property cust_id As Integer

End Class
