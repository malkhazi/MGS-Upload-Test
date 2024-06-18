Imports Newtonsoft.Json
Imports System.Data
Imports System.IO

Module Mod_SalIn

    Public Function Read_JS_SalIn() As Rt_Class
        Dim Rt_Cls1 As New Rt_Class
        Dim Rt_DataSet As New DataSet("InvoicesDataSet")
        Dim filePath As String = Application.StartupPath & $"\JsonFiles\JSon_Create_SalIn.txt"
        If My.Computer.FileSystem.DirectoryExists(Path.GetDirectoryName(filePath)) Then
            Dim fileContents As String = File.ReadAllText(filePath)

            Dim invoices As List(Of Invoice) = JsonConvert.DeserializeObject(Of List(Of Invoice))(fileContents)
            Dim invoicesTable As New DataTable("Invoices")
            Dim salInLocalDetailsTable As New DataTable("SalInLocalDetails")

            invoicesTable.Columns.Add("invoice_no", GetType(Integer))
            invoicesTable.Columns.Add("date", GetType(DateTime))
            invoicesTable.Columns.Add("wareh_code", GetType(String))
            invoicesTable.Columns.Add("doc_type", GetType(Integer))
            invoicesTable.Columns.Add("custdoc_no", GetType(String))
            invoicesTable.Columns.Add("vatcalcmod", GetType(Integer))
            invoicesTable.Columns.Add("status", GetType(Integer))
            invoicesTable.Columns.Add("dtlm", GetType(DateTime))
            invoicesTable.Columns.Add("cust_id", GetType(Integer))

            salInLocalDetailsTable.Columns.Add("invoice_no", GetType(Integer))
            salInLocalDetailsTable.Columns.Add("localcode", GetType(String))
            salInLocalDetailsTable.Columns.Add("lot_id", GetType(String))
            salInLocalDetailsTable.Columns.Add("checkId", GetType(Integer))
            salInLocalDetailsTable.Columns.Add("det_id", GetType(Integer))
            salInLocalDetailsTable.Columns.Add("price", GetType(Decimal))
            salInLocalDetailsTable.Columns.Add("qty", GetType(Decimal))
            salInLocalDetailsTable.Columns.Add("vat", GetType(Decimal))
            salInLocalDetailsTable.Columns.Add("status", GetType(Integer))
            salInLocalDetailsTable.Columns.Add("dtlm", GetType(DateTime))
            salInLocalDetailsTable.Columns.Add("cust_id", GetType(Integer))

            For Each invoice In invoices
                invoicesTable.Rows.Add(invoice.invoice_no, CDate(invoice.date), invoice.wareh_code, invoice.doc_type, invoice.custdoc_no, invoice.vatcalcmod, invoice.status, CDate(invoice.dtlm), invoice.cust_id)
                For Each detail In invoice.salInLocalDetail
                    salInLocalDetailsTable.Rows.Add(invoice.invoice_no, detail.localcode.id, detail.lot_id, detail.checkId, detail.det_id, detail.price, detail.qty, detail.vat, detail.status, CDate(detail.dtlm), detail.cust_id)
                Next
            Next

            Rt_DataSet.Tables.Add(invoicesTable)
            Rt_DataSet.Tables.Add(salInLocalDetailsTable)

            'Dim relation As New DataRelation("Invoice_SalInLocalDetails", invoicesTable.Columns("invoice_no"), salInLocalDetailsTable.Columns("invoice_no"))
            'Rt_DataSet.Relations.Add(relation)
            Rt_Cls1.Rt_flg = True
        End If
        Rt_Cls1.Rt_Ds = Rt_DataSet
        Return Rt_Cls1
    End Function

    Private Class Invoice
        Public Property invoice_no As Integer
        Public Property [date] As String
        Public Property wareh_code As String
        Public Property doc_type As Integer
        Public Property custdoc_no As String
        Public Property vatcalcmod As Integer
        Public Property status As Integer
        Public Property dtlm As String
        Public Property cust_id As Integer
        Public Property salInLocalDetail As List(Of SalInLocalDetail)
    End Class

    Private Class SalInLocalDetail
        Public Property localcode As LocalCode
        Public Property lot_id As String
        Public Property checkId As Integer
        Public Property det_id As Integer
        Public Property price As Decimal
        Public Property qty As Decimal
        Public Property vat As Decimal
        Public Property status As Integer
        Public Property dtlm As String
        Public Property cust_id As Integer
    End Class

    Private Class LocalCode
        Public Property id As String
    End Class
End Module
