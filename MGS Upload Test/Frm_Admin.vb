Imports System.Net

Public Class Frm_Admin
    Private cfls_ClientSecret1 As String
    Private cfls_ClientSecret2 As String
    Private pass8 As String

    Private Sub Frm_Admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.AppIcon
        'MSSQL
        Me.Tb_serv_ip.Text = My_Get_Setting("", "", "serv_ip", "127.0.0.1")
        Me.Tb_baza.Text = My_Get_Setting("", "", "baza", "")
        Me.Tb_TmpBaza.Text = My_Get_Setting("", "", "TmpBaza", "")
        Me.Tb_serv_user.Text = My_Get_Setting("", "", "serv_use", "")
        pass8 = wrapper.DecryptData(My_Get_Setting("", "", "serv_ps", ""))
        Me.Tb_serv_port.Text = My_Get_Setting("", "", "serv_port", "0")

        Tb_ClientNik1.Text = My_Get_Setting("", "", "ClientNik1", "")
        Tb_ClientId1.Text = My_Get_Setting("", "", "ClientId1", "")
        cfls_ClientSecret1 = wrapper.DecryptData(My_Get_Setting("", "", "ClientSecret1", ""))
        Tb_cust_id1.Text = My_Get_Setting("", "", "cust_id1", "0")

        Tb_ClientNik2.Text = My_Get_Setting("", "", "ClientNik2", "")
        Tb_ClientId2.Text = My_Get_Setting("", "", "ClientId2", "")
        cfls_ClientSecret2 = wrapper.DecryptData(My_Get_Setting("", "", "ClientSecret2", ""))
        Tb_cust_id2.Text = My_Get_Setting("", "", "cust_id2", "0")

        Mtb_time.Text = My_Get_Setting("", "", "cur_time", "23:30")
        Tb_IP1.Text = My_Get_Setting("", "", "ServIP1", "")
        Tb_IP2.Text = My_Get_Setting("", "", "ServIP2", "")
        If My_Get_Setting("", "", "cur_baza_id", "1") = 1 Then
            Me.Rb_addr1.Checked = True
        Else
            Me.Rb_addr2.Checked = True
        End If
        If CInt(My_Get_Setting("", "", "IPType", "1")) = 1 Then
            Me.Rb_http1.Checked = True
        Else
            Me.Rb_https1.Checked = True
        End If
        'მომწოდებელი
        Me.Tb_key_klGm.Text = My_Get_Setting("", "", "momc_sn", "404888528")
    End Sub

    Private Sub Bt_Save_Click(sender As Object, e As EventArgs) Handles Bt_Save.Click
        If Not (Me.Tb_serv_ps.Text.Trim.Length > 0 Or pass8.Length > 0) Then
            MsgBox("მიუთითედ MS SQL Server-ის პაროლი!", , "")
            Return
        End If
        If Tb_IP1.Text.Trim.Length = 0 Then
            MsgBox("ჩაწერეთ IP მისამართი!",, "")
            Return
        End If

        'MS SQL Server
        My_Save_Setting("", "", "serv_ip", Me.Tb_serv_ip.Text.Trim)
        My_Save_Setting("", "", "serv_port", Me.Tb_serv_port.Text.Trim)
        My_Save_Setting("", "", "baza", Me.Tb_baza.Text.Trim.Trim)
        My_Save_Setting("", "", "TmpBaza", Me.Tb_TmpBaza.Text.Trim.Trim)
        My_Save_Setting("", "", "serv_use", Me.Tb_serv_user.Text.Trim)
        If Me.Tb_serv_ps.Text.Trim.Length > 0 Then
            My_Save_Setting("", "", "serv_ps", wrapper.EncryptData(Me.Tb_serv_ps.Text.Trim))
        End If

        My_Save_Setting("", "", "ClientNik1", Me.Tb_ClientNik1.Text.Trim)
        My_Save_Setting("", "", "ClientId1", Me.Tb_ClientId1.Text.Trim)
        If Me.Tb_ClientSecret1.Text.Trim.Length > 0 Then
            My_Save_Setting("", "", "ClientSecret1", wrapper.EncryptData(Me.Tb_ClientSecret1.Text.Trim))
        End If
        My_Save_Setting("", "", "cust_id1", Me.Tb_cust_id1.Text.Trim)

        My_Save_Setting("", "", "ClientNik2", Me.Tb_ClientNik2.Text.Trim)
        My_Save_Setting("", "", "ClientId2", Me.Tb_ClientId2.Text.Trim)
        If Me.Tb_ClientSecret2.Text.Trim.Length > 0 Then
            My_Save_Setting("", "", "ClientSecret2", wrapper.EncryptData(Me.Tb_ClientSecret2.Text.Trim))
        End If
        My_Save_Setting("", "", "cust_id2", Me.Tb_cust_id2.Text.Trim)


        Dim dt_time11 As DateTime = Convert.ToDateTime(Me.Mtb_time.Text)
        My_Save_Setting("", "", "cur_time", dt_time11.ToString("HH:mm"))

        'Dim MyIpAddress1 As IPAddress = IPAddress.Parse(Tb_IP1.Text.Trim)
        My_Save_Setting("", "", "ServIP1", Me.Tb_IP1.Text.ToString.Trim)
        If Tb_IP2.Text.Trim.Length > 0 Then
            'Dim MyIpAddress2 As IPAddress = IPAddress.Parse(Tb_IP2.Text.Trim)
            My_Save_Setting("", "", "ServIP2", Me.Tb_IP2.Text.ToString.Trim)
        End If

        If Tb_ClientSecret1.Text.Trim.Length > 0 Then
            My_Save_Setting("", "", "ClientSecret", wrapper.EncryptData(Tb_ClientSecret1.Text.Trim))
        End If
        If Me.Rb_http1.Checked Then
            My_Save_Setting("", "", "IPType", "1")
        Else
            My_Save_Setting("", "", "IPType", "2")
        End If
        If Me.Rb_addr1.Checked Then
            My_Save_Setting("", "", "cur_baza_id", "1")
        Else
            My_Save_Setting("", "", "cur_baza_id", "2")
        End If

        'მომწოდებელი
        My_Save_Setting("", "", "momc_sn", Me.Tb_key_klGm.Text.Trim)

        Me.DialogResult = DialogResult.OK
    End Sub

    Public Function IsAddressValid(ByVal addrString As String) As Boolean
        Dim address As IPAddress = Nothing
        Return IPAddress.TryParse(addrString, address)
    End Function

    Private Sub Bt_mssql_shem_Click(sender As Object, e As EventArgs) Handles Bt_mssql_shem.Click
        If pass8.Length = 0 And Me.Tb_serv_ps.Text.Trim.Length = 0 Then
            MsgBox("მიუთითედ პაროლი", , "")
            Return
        End If
        Dim connection_string As String = String.Format("Provider=sqloledb;Data Source={0},{4};Network Library=DBMSSOCN;Initial Catalog={1};User Id={2};Password={3};",
                              Me.Tb_serv_ip.Text.Trim, Me.Tb_baza.Text.Trim, Me.Tb_serv_user.Text.Trim,
                              If(Me.Tb_serv_ps.Text.Trim.Length = 0, pass8, Me.Tb_serv_ps.Text.Trim),
                              Me.Tb_serv_port.Text.Trim)
        Dim cn1 As OleDb.OleDbConnection = New OleDb.OleDbConnection(connection_string)
        Try
            cn1.Open()
            cn1.Close()
            MsgBox("დაკავშირება შედგა")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class