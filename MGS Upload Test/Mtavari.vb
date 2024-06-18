Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Mtavari
    Private flg_exit As Boolean = False

    Private cflg_DetCamot1 As Boolean = True

    Private cfli_SalOutIndx1 As Integer = -1
    Private cfli_SalInIndx1 As Integer = -1
    Private cfli_OrderIndx1 As Integer = -1
    Private cfli_OutletDebtIndx1 As Integer = -1
    Private cfli_SalOutDetIndx1 As Integer = -1
    Private cfli_SalInDetIndx1 As Integer = -1
    Private cfli_OrderDetIndx1 As Integer = -1
    Private cfli_OutletDebtDetIndx1 As Integer = -1

    Private cDT_LocalProduct As New DataTable
    Private cDV_LocalProduct As DataView

    Private cDT_ParentCompany As New DataTable
    Private cDV_ParentCompany As DataView

    Private cDT_CheckSumSalIn As New DataTable
    Private cDV_CheckSumSalIn As DataView

    Private cDT_SalIn As New DataTable
    Private cDV_SalIn As DataView

    Private cDT_Outlet As New DataTable
    Private cDV_Outlet As DataView

    Private cDT_SalOut As New DataTable
    Private cDV_SalOut As DataView

    Private cDT_CheckSumSalOut As New DataTable
    Private cDV_CheckSumSalOut As DataView

    Private cDT_ArchivedLocalStock As New DataTable
    Private cDV_ArchivedLocalStock As DataView

    Private cDT_Order As New DataTable
    Private cDV_Order As DataView

    Private cDT_CheckSumOrder As New DataTable
    Private cDV_CheckSumOrder As DataView

    Private cDT_OutletDebt As New DataTable
    Private cDV_OutletDebt As DataView

    Private cDT_CheckSumOutletDebt As New DataTable
    Private cDV_CheckSumOutletDebt As DataView

    Private cjs_FindData As New List(Of FindData)

    Private cfli_indCheckSum1 As Integer = -1

    Private Sub Mtavari_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.AppIcon
        Me.NotifyIcon1.Icon = My.Resources.AppIcon
        Application.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")
        '
        '
        cDT_goods.Columns.Add("id", Type.GetType("System.String"))
        cDT_goods.Columns.Add("checkId", Type.GetType("System.Int64"))
        cDT_goods.Columns.Add("det_id", Type.GetType("System.Int64"))
        cDT_goods.Columns.Add("localcode", Type.GetType("System.String"))
        cDT_goods.Columns.Add("name", Type.GetType("System.String"))
        cDT_goods.Columns.Add("price", Type.GetType("System.Decimal"))
        cDT_goods.Columns.Add("qty", Type.GetType("System.Decimal"))


        cDT_OutletDebtDet.Columns.Add("id", Type.GetType("System.String"))
        cDT_OutletDebtDet.Columns.Add("checkId", Type.GetType("System.Int64"))
        cDT_OutletDebtDet.Columns.Add("det_id", Type.GetType("System.Int64"))
        cDT_OutletDebtDet.Columns.Add("date", Type.GetType("System.DateTime"))
        cDT_OutletDebtDet.Columns.Add("debt", Type.GetType("System.Decimal"))
        cDT_OutletDebtDet.Columns.Add("qty", Type.GetType("System.Decimal"))
        cDT_OutletDebtDet.Columns.Add("debtypcode", Type.GetType("System.Int32"))
        cDT_OutletDebtDet.Columns.Add("invoice_no", Type.GetType("System.Int64"))
        '
        Nud_outlet.Value = CInt(My_Get_Setting("", "", "Nud_outlet", "5"))
        Nud_LocalStock.Value = CInt(My_Get_Setting("", "", "Nud_LocalStock", "100"))
        Nud_ParentCompany.Value = CInt(My_Get_Setting("", "", "Nud_ParentCompany", "20"))

        Me.Dtp_01.Value = CDate(My_Get_Setting("", "", "Dtp_01", DateTime.Now.ToString("yyyy-MM-dd")))
        Me.Dtp_02.Value = CDate(My_Get_Setting("", "", "Dtp_02", DateTime.Now.ToString("yyyy-MM-dd")))
        Me.Dtp_Find_dtlm1.Value = CDate(My_Get_Setting("", "", "Dtp_Find_dtlm1", DateTime.Now.ToString("yyyy-MM-dd")))
        Me.Dtp_Find_dtlm2.Value = CDate(My_Get_Setting("", "", "Dtp_Find_dtlm2", DateTime.Now.ToString("yyyy-MM-dd")))
        Me.Chk_dt2.Checked = CBool(My_Get_Setting("", "", "Chk_dt2", "False"))
        Me.Chk_Find_dtlm.Checked = CBool(My_Get_Setting("", "", "Chk_Find_dtlm", "False"))

        Me.Dtp_CheckSum1.Value = CDate(My_Get_Setting("", "", "Dtp_CheckSum1", DateTime.Now.ToString("yyyy-MM-dd")))
        Me.Dtp_CheckSum2.Value = CDate(My_Get_Setting("", "", "Dtp_CheckSum2", DateTime.Now.ToString("yyyy-MM-dd")))

        Load_Sack()
        Load_Flg1()

        Me.Tsb_User1.Text = M_ClientNik1
        Me.Tsb_User2.Text = M_ClientNik2

        'Tmr_Tick.Enabled = True
        If Not System.Diagnostics.Debugger.IsAttached Then
            'Tmr_start.Enabled = True
        End If

        Dim Dir_nm1 As String = Application.StartupPath & "\Logs\"
        If Not My.Computer.FileSystem.DirectoryExists(Path.GetDirectoryName(Dir_nm1)) Then
            Directory.CreateDirectory(Dir_nm1)
        End If
        Dir_nm1 = Application.StartupPath & "\JsonFiles\"
        If Not My.Computer.FileSystem.DirectoryExists(Dir_nm1) Then
            Directory.CreateDirectory(Path.GetDirectoryName(Dir_nm1))
        End If
    End Sub
    Private Sub Mtavari_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Me.WindowState = FormWindowState.Minimized OrElse flg_exit Then
            e.Cancel = False
            Return
        Else
            'e.Cancel = True
        End If
        'Try
        '    Me.NotifyIcon1.Visible = True
        '    Me.Visible = False
        'Catch
        'End Try
    End Sub

    Private Sub Mtavari_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        'Try
        '    Me.NotifyIcon1.Visible = True
        '    Me.Visible = False
        'Catch
        'End Try
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        'Try
        '    Me.Visible = True
        '    Me.WindowState = FormWindowState.Normal
        '    Me.NotifyIcon1.Visible = False
        'Catch
        'End Try
    End Sub

    Private Sub Tmr_start_Tick(sender As Object, e As EventArgs) Handles Tmr_start.Tick
        'Try
        '    Me.NotifyIcon1.Visible = True
        '    Me.Visible = False
        '    Me.Tmr_start.Enabled = False
        'Catch
        'End Try
    End Sub

    Public Sub Get_CurUser(Pr_vrt1 As Integer, Optional Pr_Save As Boolean = True)
        If Pr_vrt1 = 1 Then
            Me.Tsb_User1.Image = My.Resources.User1E
            Me.Tsb_User2.Image = My.Resources.User2d
        Else
            Me.Tsb_User1.Image = My.Resources.User1d
            Me.Tsb_User2.Image = My.Resources.User2e
        End If
        If Pr_vrt1 = 1 Then
            M_Cur_ClientNik = M_ClientNik1
            M_Cur_ClientId = M_ClientId1
            M_Cur_ClientSecret = M_ClientSecret1
            M_Cur_cust_id = M_cust_id1
        Else
            M_Cur_ClientNik = M_ClientNik2
            M_Cur_ClientId = M_ClientId2
            M_Cur_ClientSecret = M_ClientSecret2
            M_Cur_cust_id = M_cust_id2
        End If
        If Pr_Save Then
            My_Save_Setting("", "", "cur_User", Pr_vrt1)
        End If
    End Sub

    Private Sub Mtavari_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Save_Log(Me.RTb_info)
    End Sub

    Private Sub Bt_Save_Log_Click(sender As Object, e As EventArgs) Handles Bt_Save_Log.Click
        Save_Log(Me.RTb_info)
        Me.RTb_info.Text = ""
    End Sub

    Private Sub Save_Log(ByVal p_txt2 As RichTextBox)
        If p_txt2.Text.Trim.Length > 0 Then
            Dim fl_nm33 As String = Application.StartupPath & "\Logs\Log_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & ".txt"
            If Not My.Computer.FileSystem.DirectoryExists(Path.GetDirectoryName(fl_nm33)) Then
                Directory.CreateDirectory(Path.GetDirectoryName(fl_nm33))
            End If
            System.IO.File.WriteAllText(fl_nm33, p_txt2.Text)
            p_txt2.Text = ""
        End If
    End Sub

    Private Sub Tmr_Tick_Tick(sender As Object, e As EventArgs) Handles Tmr_Tick.Tick
        If Me.RTb_info.Lines.Count > 250 Then
            Save_Log(Me.RTb_info)
        End If
    End Sub

    Private Sub Tsb_Json_Files_Click(sender As Object, e As EventArgs) Handles Tsb_Json_Files.Click
        Process.Start("explorer.exe", Chr(34) & Application.StartupPath & "\JsonFiles" & Chr(34))
    End Sub

    Public Sub Load_Sack(Optional cp_01 As Boolean = False)
        M_time_Str11 = My_Get_Setting("", "", "cur_time", "23:30")

        'momwod
        M_momc_SN = My_Get_Setting("", "", "momc_sn", "404888528")

        SQL_conn_string = String.Format("Data Source ={0},{4}; Initial Catalog ={1}; User ID={2}; Password={3}",
                                My_Get_Setting("", "", "serv_ip", "").Trim,
                                My_Get_Setting("", "", "baza", "").Trim,
                                My_Get_Setting("", "", "serv_use", "").Trim,
                                wrapper.DecryptData(My_Get_Setting("", "", "serv_ps", "").Trim),
                                My_Get_Setting("", "", "serv_port", "0").Trim)
        SqlClient_Tmp_con_string = String.Format("Data Source ={0},{4}; Initial Catalog ={1}; User ID={2}; Password={3}",
                                My_Get_Setting("", "", "serv_ip", "").Trim,
                                My_Get_Setting("", "", "tmpbaza", "").Trim,
                                My_Get_Setting("", "", "serv_use", "").Trim,
                                wrapper.DecryptData(My_Get_Setting("", "", "serv_ps", "").Trim),
                                My_Get_Setting("", "", "serv_port", "0").Trim)

        M_ClientNik1 = My_Get_Setting("", "", "ClientNik1", "")
        M_ClientId1 = My_Get_Setting("", "", "ClientId1", "")
        M_ClientSecret1 = wrapper.DecryptData(My_Get_Setting("", "", "ClientSecret1", ""))
        M_cust_id1 = My_Get_Setting("", "", "cust_id1", "0")

        M_ClientNik2 = My_Get_Setting("", "", "ClientNik2", "")
        M_ClientId2 = My_Get_Setting("", "", "ClientId2", "")
        M_ClientSecret2 = wrapper.DecryptData(My_Get_Setting("", "", "ClientSecret2", ""))
        M_cust_id2 = My_Get_Setting("", "", "cust_id2", "0")

        Get_CurServer(CInt(My_Get_Setting("", "", "cur_baza_id", "1")), False)
        Get_CurUser(CInt(My_Get_Setting("", "", "cur_User", "1")), False)

    End Sub


    Public Sub Load_Flg1()
        Me.Tsl_time.Text = "ატვირთვის დრო: " & M_time_Str11
    End Sub
    Private Sub TSB_params_Click(sender As Object, e As EventArgs) Handles TSB_params.Click
        M_flg_atv1 = False
        Dim frm_01 As New Frm_Admin
        If frm_01.ShowDialog() = DialogResult.OK Then
            Load_Sack()
        End If
        frm_01.Dispose()
        M_flg_atv1 = True
    End Sub

    Private Sub TSB_exit_Click(sender As Object, e As EventArgs) Handles TSB_exit.Click
        flg_exit = True
        Me.Close()
    End Sub

    Public Sub UpdateProgressBar0(value As Integer)
        If Pbr_00.InvokeRequired Then
            Pbr_00.Invoke(Sub() UpdateProgressBar0(value))
        Else
            Pbr_00.Value = value
        End If
    End Sub
    Public Sub UpdateProgressBar1(value As Integer)
        If Pbr_01.InvokeRequired Then
            Pbr_01.Invoke(Sub() UpdateProgressBar1(value))
        Else
            Pbr_01.Value = value
        End If
    End Sub


    Private Sub Bt_copy1_Click(sender As Object, e As EventArgs) Handles Bt_copy1.Click
        sender.enabled = False
        Dim sData As String = ""
        Dim dm_txt1 As String = ""

        For Each drv_1 As DataGridViewColumn In Me.DGV_LocalProduct.Columns
            If drv_1.Visible Then
                sData &= dm_txt1 & drv_1.HeaderText
                dm_txt1 = vbTab
            End If
        Next
        sData &= vbCr
        For Each drw1 As DataGridViewRow In Me.DGV_LocalProduct.Rows

            dm_txt1 = ""
            For Each cl1 As DataGridViewCell In drw1.Cells
                If Me.DGV_LocalProduct.Columns(cl1.ColumnIndex).Visible Then
                    sData &= dm_txt1 & cl1.Value.ToString
                    dm_txt1 = vbTab
                End If
            Next
            sData &= vbCr
        Next
        Clipboard.Clear()
        Clipboard.SetText(sData)
        sender.enabled = True
        MsgBox("კოპირება დასრულდა", , "")
    End Sub

    Private Sub Tsb_IP1_Click(sender As Object, e As EventArgs) Handles Tsb_IP1.Click, Tsb_IP2.Click
        If sender.name = "Tsb_IP2" Then
            Get_CurServer(2)
        Else
            Get_CurServer(1)
        End If
    End Sub

    Private Sub Get_CurServer(Pr_vrt1 As Integer, Optional Pr_Save As Boolean = True)
        If Pr_vrt1 = 1 Then
            Tsb_IP1.Image = My.Resources.IP1e
            Tsb_IP2.Image = My.Resources.IP2d
        Else
            Tsb_IP1.Image = My.Resources.IP1d
            Tsb_IP2.Image = My.Resources.IP2e
        End If
        If CInt(My_Get_Setting("", "", "IPType", "1")) = 1 Then
            M_SrvIP = "http://"
        Else
            M_SrvIP = "https://"
        End If
        If Pr_vrt1 = 1 Then
            M_SrvIP &= My_Get_Setting("", "", "ServIP1", "")
        Else
            M_SrvIP &= My_Get_Setting("", "", "ServIP2", "")
        End If
        If Pr_Save Then
            My_Get_Setting("", "", "cur_baza_id", Pr_vrt1)
        End If
    End Sub

    Private Sub Tsb_User1_Click(sender As Object, e As EventArgs) Handles Tsb_User1.Click, Tsb_User2.Click
        If sender.name = "Tsb_User2" Then
            Get_CurUser(2)
        Else
            Get_CurUser(1)
        End If
    End Sub


    Private Sub Chk_ParentCompany_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_ParentCompany.CheckedChanged
        For Each dr21 As DataRow In cDT_ParentCompany.Rows
            dr21("sh1") = CType(sender, CheckBox).Checked
        Next
    End Sub

    Private Sub Chk_Outlet_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Outlet.CheckedChanged
        For Each dr21 As DataRow In cDT_Outlet.Rows
            dr21("sh1") = CType(sender, CheckBox).Checked
        Next
    End Sub

    Private Sub Chk_SalIn_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_SalIn.CheckedChanged
        For Each dr21 As DataRow In cDT_SalIn.Rows
            dr21("sh1") = CType(sender, CheckBox).Checked
        Next
    End Sub

    Private Sub Chk_SalOut_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_SalOut.CheckedChanged
        For Each dr21 As DataRow In cDT_SalOut.Rows
            dr21("sh1") = CType(sender, CheckBox).Checked
        Next
    End Sub

    Private Sub Chk_ArchivedLocalStock_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_ArchivedLocalStock.CheckedChanged
        For Each dr21 As DataRow In cDT_ArchivedLocalStock.Rows
            dr21("sh1") = CType(sender, CheckBox).Checked
        Next
    End Sub

    Private Sub Chk_Order_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Order.CheckedChanged
        For Each dr21 As DataRow In cDT_Order.Rows
            dr21("sh1") = CType(sender, CheckBox).Checked
        Next
    End Sub

    Private Sub Chk_OutletDebt_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_OutletDebt.CheckedChanged
        For Each dr21 As DataRow In cDT_OutletDebt.Rows
            dr21("sh1") = CType(sender, CheckBox).Checked
        Next
    End Sub

    Private Async Sub Bt_LocalProduct_Click(sender As Object, e As EventArgs) Handles Bt_LocalProduct.Click
        sender.Enabled = False
        Me.DGV_LocalProduct.DataSource = ""
        cDT_LocalProduct = New DataTable
        Dim rt1 As Rt_Class = Await JS_DownloadData("prx_LocalProduct")
        If rt1.Rt_flg Then
            cDT_LocalProduct = rt1.Rt_Dt
        Else
            MsgBox(rt1.Rt_txt,, "")
        End If
        cDV_LocalProduct = cDT_LocalProduct.DefaultView
        Me.DGV_LocalProduct.DataSource = cDV_LocalProduct
        Me.Lb_LocalProduct.Text = String.Format("სულ {0} ჩანაწერი", cDT_LocalProduct.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_prx_ParentCompany_Click(sender As Object, e As EventArgs) Handles Bt_prx_ParentCompany.Click
        sender.Enabled = False
        Me.DGV_ParentCompany.DataSource = ""
        cDT_ParentCompany = New DataTable
        cDT_ParentCompany.Columns.Add("sh1", Type.GetType("System.Boolean"))
        Dim rt1 As Rt_Class = Await JS_DownloadData("prx_ParentCompany")
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_ParentCompany = rt1.Rt_Dt
        End If

        cDV_ParentCompany = cDT_ParentCompany.DefaultView
        Me.DGV_ParentCompany.DataSource = cDV_ParentCompany
        Me.Lb_ParentCompany.Text = String.Format("სულ {0} ჩანაწერი", cDT_ParentCompany.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_prx_Outlet_Click(sender As Object, e As EventArgs) Handles Bt_prx_Outlet.Click
        sender.Enabled = False
        Me.DGV_Outlet.DataSource = ""
        cDT_Outlet = New DataTable
        cDT_Outlet.Columns.Add("sh1", Type.GetType("System.Boolean"))
        Dim rt1 As Rt_Class = Await JS_DownloadData("prx_Outlet")
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_Outlet = rt1.Rt_Dt
        End If

        cDV_Outlet = cDT_Outlet.DefaultView
        Me.DGV_Outlet.DataSource = cDV_Outlet
        Me.Lb_Outlet.Text = String.Format("სულ {0} ჩანაწერი", cDT_Outlet.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_prx_SalIn_Click(sender As Object, e As EventArgs) Handles Bt_prx_SalIn.Click
        sender.Enabled = False
        Me.DGV_SalIn.DataSource = ""
        cfli_SalInDetIndx1 = -1
        cDT_SalIn = New DataTable
        cDT_SalIn.Columns.Add("sh1", Type.GetType("System.Boolean"))
        Dim rt1 As Rt_Class = Await JS_DownloadData("prx_SalIn")
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_SalIn = rt1.Rt_Dt
        End If

        cDV_SalIn = cDT_SalIn.DefaultView
        Me.DGV_SalIn.DataSource = cDV_SalIn
        Me.Lb_SalIn.Text = String.Format("სულ {0} ჩანაწერი", cDT_SalIn.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_prx_SalOut_Click(sender As Object, e As EventArgs) Handles Bt_prx_SalOut.Click
        sender.Enabled = False
        Me.Dgv_SalOut.DataSource = ""
        cfli_SalOutDetIndx1 = -1
        cDT_SalOut = New DataTable
        cDT_SalOut.Columns.Add("sh1", Type.GetType("System.Boolean"))
        Dim rt1 As Rt_Class = Await JS_DownloadData("prx_SalOut")
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_SalOut = rt1.Rt_Dt
        End If

        cDV_SalOut = cDT_SalOut.DefaultView
        Me.Dgv_SalOut.DataSource = cDV_SalOut
        Me.Lb_SalOut.Text = String.Format("სულ {0} ჩანაწერი", cDT_SalOut.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_prx_ArchivedLocalStock_Click(sender As Object, e As EventArgs) Handles Bt_prx_ArchivedLocalStock.Click
        sender.Enabled = False
        Me.DGV_ArchivedLocalStock.DataSource = ""
        cDT_ArchivedLocalStock = New DataTable
        cDT_ArchivedLocalStock.Columns.Add("sh1", Type.GetType("System.Boolean"))
        Dim rt1 As Rt_Class = Await JS_DownloadData("prx_ArchivedLocalStock")
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_ArchivedLocalStock = rt1.Rt_Dt
        End If

        cDV_ArchivedLocalStock = cDT_ArchivedLocalStock.DefaultView
        Me.DGV_ArchivedLocalStock.DataSource = cDV_ArchivedLocalStock
        Me.Lb_ArchivedLocalStock.Text = String.Format("სულ {0} ჩანაწერი", cDT_ArchivedLocalStock.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_prx_Order_Click(sender As Object, e As EventArgs) Handles Bt_prx_Order.Click
        sender.Enabled = False
        Me.Dgv_Order.DataSource = ""
        cfli_OrderDetIndx1 = -1
        cDT_Order = New DataTable
        cDT_Order.Columns.Add("sh1", Type.GetType("System.Boolean"))
        Dim rt1 As Rt_Class = Await JS_DownloadData("prx_Order")
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_Order = rt1.Rt_Dt
        End If

        cDV_Order = cDT_Order.DefaultView
        Me.Dgv_Order.DataSource = cDV_Order
        Me.Lb_Order.Text = String.Format("სულ {0} ჩანაწერი", cDT_Order.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_prx_OutletDebt_Click(sender As Object, e As EventArgs) Handles Bt_prx_OutletDebt.Click
        sender.Enabled = False
        Me.Dgv_OutletDebt.DataSource = ""
        cfli_OutletDebtDetIndx1 = -1
        cDT_OutletDebt = New DataTable
        cDT_OutletDebt.Columns.Add("sh1", Type.GetType("System.Boolean"))
        Dim rt1 As Rt_Class = Await JS_DownloadData("prx_OutletDebt")
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_OutletDebt = rt1.Rt_Dt
        End If
        cDV_OutletDebt = cDT_OutletDebt.DefaultView
        Me.Dgv_OutletDebt.DataSource = cDV_OutletDebt
        Me.Lb_OutletDebt.Text = String.Format("სულ {0} ჩანაწერი", cDT_OutletDebt.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindParentCompany_Click(sender As Object, e As EventArgs) Handles Bt_FindParentCompany.Click
        sender.Enabled = False
        Me.DGV_ParentCompany.DataSource = ""
        cDT_ParentCompany = New DataTable
        cDT_ParentCompany.Columns.Add("sh1", Type.GetType("System.Boolean"))
        cjs_FindData.Clear()
        cjs_FindData.Add(New FindData With {.[property] = "cust_id",
                                            .[operator] = "=",
                                            .value = M_Cur_cust_id})
        Dim fnd_str1 As String = "{""filter"":{""conditions"":" & JsonConvert.SerializeObject(cjs_FindData) & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadFindData("prx_ParentCompany", fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_ParentCompany = rt1.Rt_Dt
        End If
        cDV_ParentCompany = cDT_ParentCompany.DefaultView
        Me.DGV_ParentCompany.DataSource = cDV_ParentCompany
        Me.Lb_ParentCompany.Text = String.Format("სულ {0} ჩანაწერი", cDT_ParentCompany.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindOutlet_Click(sender As Object, e As EventArgs) Handles Bt_FindOutlet.Click
        sender.Enabled = False
        Me.DGV_Outlet.DataSource = ""
        cDT_Outlet = New DataTable
        cDT_Outlet.Columns.Add("sh1", Type.GetType("System.Boolean"))
        cjs_FindData.Clear()
        cjs_FindData.Add(New FindData With {.[property] = "cust_id",
                                            .[operator] = "=",
                                            .value = M_Cur_cust_id})
        Dim fnd_str1 As String = "{""filter"":{""conditions"":" & JsonConvert.SerializeObject(cjs_FindData) & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadFindData("prx_Outlet", fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_Outlet = rt1.Rt_Dt
        End If
        cDV_Outlet = cDT_Outlet.DefaultView
        Me.DGV_Outlet.DataSource = cDV_Outlet
        Me.Lb_Outlet.Text = String.Format("სულ {0} ჩანაწერი", cDT_Outlet.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindSalIn_Click(sender As Object, e As EventArgs) Handles Bt_FindSalIn.Click
        sender.Enabled = False
        Me.DGV_SalIn.DataSource = ""
        cfli_SalInDetIndx1 = -1
        cDT_SalIn = New DataTable
        cDT_SalIn.Columns.Add("sh1", Type.GetType("System.Boolean"))
        cjs_FindData.Clear()
        cjs_FindData.Add(New FindData With {.[property] = "cust_id",
                                            .[operator] = "=",
                                            .value = M_Cur_cust_id})
        Dim fnd_str1 As String = "{""filter"":{""conditions"":" & JsonConvert.SerializeObject(cjs_FindData) & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadFindData("prx_SalIn", fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_SalIn = rt1.Rt_Dt
        End If
        cDV_SalIn = cDT_SalIn.DefaultView
        Me.DGV_SalIn.DataSource = cDV_SalIn
        Me.Lb_SalIn.Text = String.Format("სულ {0} ჩანაწერი", cDT_SalIn.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindSalOut_Click(sender As Object, e As EventArgs) Handles Bt_FindSalOut.Click
        sender.Enabled = False
        Me.Dgv_SalOut.DataSource = ""
        cfli_SalOutDetIndx1 = -1
        cDT_SalOut = New DataTable
        cDT_SalOut.Columns.Add("sh1", Type.GetType("System.Boolean"))
        cjs_FindData.Clear()
        cjs_FindData.Add(New FindData With {.[property] = "cust_id",
                                            .[operator] = "=",
                                            .value = M_Cur_cust_id})
        Dim fnd_str1 As String = "{""filter"":{""conditions"":" & JsonConvert.SerializeObject(cjs_FindData) & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadFindData("prx_SalOut", fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_SalOut = rt1.Rt_Dt
        End If
        cDV_SalOut = cDT_SalOut.DefaultView
        Me.Dgv_SalOut.DataSource = cDV_SalOut
        Me.Lb_SalOut.Text = String.Format("სულ {0} ჩანაწერი", cDT_SalOut.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindArchivedLocalStock_Click(sender As Object, e As EventArgs) Handles Bt_FindArchivedLocalStock.Click
        sender.Enabled = False
        Me.DGV_ArchivedLocalStock.DataSource = ""
        cDT_ArchivedLocalStock = New DataTable
        cDT_ArchivedLocalStock.Columns.Add("sh1", Type.GetType("System.Boolean"))
        cjs_FindData.Clear()
        cjs_FindData.Add(New FindData With {.[property] = "cust_id",
                                            .[operator] = "=",
                                            .value = M_Cur_cust_id})
        Dim fnd_str1 As String = "{""filter"":{""conditions"":" & JsonConvert.SerializeObject(cjs_FindData) & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadFindData("prx_ArchivedLocalStock", fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_ArchivedLocalStock = rt1.Rt_Dt
        End If
        cDV_ArchivedLocalStock = cDT_ArchivedLocalStock.DefaultView
        Me.DGV_ArchivedLocalStock.DataSource = cDV_ArchivedLocalStock
        Me.Lb_ArchivedLocalStock.Text = String.Format("სულ {0} ჩანაწერი", cDT_ArchivedLocalStock.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindOrder_Click(sender As Object, e As EventArgs) Handles Bt_FindOrder.Click
        sender.Enabled = False
        Me.Dgv_Order.DataSource = ""
        cfli_OrderDetIndx1 = -1
        cDT_Order = New DataTable
        cDT_Order.Columns.Add("sh1", Type.GetType("System.Boolean"))
        cjs_FindData.Clear()
        cjs_FindData.Add(New FindData With {.[property] = "cust_id",
                                            .[operator] = "=",
                                            .value = M_Cur_cust_id})
        Dim fnd_str1 As String = "{""filter"":{""conditions"":" & JsonConvert.SerializeObject(cjs_FindData) & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadFindData("prx_Order", fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_Order = rt1.Rt_Dt
        End If
        cDV_Order = cDT_Order.DefaultView
        Me.Dgv_Order.DataSource = cDV_Order
        Me.Lb_Order.Text = String.Format("სულ {0} ჩანაწერი", cDT_Order.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindOutletDebt_Click(sender As Object, e As EventArgs) Handles Bt_FindOutletDebt.Click
        sender.Enabled = False
        Me.Dgv_OutletDebt.DataSource = ""
        cfli_OutletDebtDetIndx1 = -1
        cDT_OutletDebt = New DataTable
        cDT_OutletDebt.Columns.Add("sh1", Type.GetType("System.Boolean"))
        cjs_FindData.Clear()
        cjs_FindData.Add(New FindData With {.[property] = "cust_id",
                                            .[operator] = "=",
                                            .value = M_Cur_cust_id})
        Dim fnd_str1 As String = "{""filter"":{""conditions"":" & JsonConvert.SerializeObject(cjs_FindData) & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadFindData("prx_OutletDebt", fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_OutletDebt = rt1.Rt_Dt
        End If
        cDV_OutletDebt = cDT_OutletDebt.DefaultView
        Me.Dgv_OutletDebt.DataSource = cDV_OutletDebt
        Me.Lb_OutletDebt.Text = String.Format("სულ {0} ჩანაწერი", cDT_OutletDebt.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindDTLMParentCompany_Click(sender As Object, e As EventArgs) Handles Bt_FindDTLMParentCompany.Click
        sender.Enabled = False
        My_Save_Setting("", "", "Dtp_Find_dtlm1", Me.Dtp_Find_dtlm1.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Dtp_Find_dtlm2", Me.Dtp_Find_dtlm2.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Chk_Find_dtlm", Me.Chk_Find_dtlm.Checked)
        Me.DGV_ParentCompany.DataSource = ""
        cDT_ParentCompany = New DataTable
        cDT_ParentCompany.Columns.Add("sh1", Type.GetType("System.Boolean"))
        cjs_FindData.Clear()
        cjs_FindData.Add(New FindData With {.[property] = "cust_id",
                                            .[operator] = "=",
                                            .value = M_Cur_cust_id})
        cjs_FindData.Add(New FindData With {.[property] = "dtlm",
                                            .[operator] = ">=",
                                            .value = Me.Dtp_Find_dtlm1.Value.ToString("yyyy-MM-ddTHH:mm:ss")})
        If Chk_Find_dtlm.Checked Then
            cjs_FindData.Add(New FindData With {.[property] = "dtlm",
                                            .[operator] = "<=",
                                            .value = Me.Dtp_Find_dtlm2.Value.ToString("yyyy-MM-ddTHH:mm:ss")})
        End If
        Dim fnd_str1 As String = "{""filter"":{""conditions"":" & JsonConvert.SerializeObject(cjs_FindData) & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadFindData("prx_ParentCompany", fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_ParentCompany = rt1.Rt_Dt
        End If
        cDV_ParentCompany = cDT_ParentCompany.DefaultView
        Me.DGV_ParentCompany.DataSource = cDV_ParentCompany
        Me.Lb_ParentCompany.Text = String.Format("სულ {0} ჩანაწერი", cDT_ParentCompany.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindDTLMOutlet_Click(sender As Object, e As EventArgs) Handles Bt_FindDTLMOutlet.Click
        sender.Enabled = False
        My_Save_Setting("", "", "Dtp_Find_dtlm1", Me.Dtp_Find_dtlm1.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Dtp_Find_dtlm2", Me.Dtp_Find_dtlm2.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Chk_Find_dtlm", Me.Chk_Find_dtlm.Checked)
        Me.DGV_Outlet.DataSource = ""
        cDT_Outlet = New DataTable
        cDT_Outlet.Columns.Add("sh1", Type.GetType("System.Boolean"))
        cjs_FindData.Clear()
        cjs_FindData.Add(New FindData With {.[property] = "cust_id",
                                            .[operator] = "=",
                                            .value = M_Cur_cust_id})
        cjs_FindData.Add(New FindData With {.[property] = "dtlm",
                                            .[operator] = ">=",
                                            .value = Me.Dtp_Find_dtlm1.Value.ToString("yyyy-MM-ddTHH:mm:ss")})
        If Chk_Find_dtlm.Checked Then
            cjs_FindData.Add(New FindData With {.[property] = "dtlm",
                                            .[operator] = "<=",
                                            .value = Me.Dtp_Find_dtlm2.Value.ToString("yyyy-MM-ddTHH:mm:ss")})
        End If
        Dim fnd_str1 As String = "{""filter"":{""conditions"":" & JsonConvert.SerializeObject(cjs_FindData) & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadFindData("prx_Outlet", fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_Outlet = rt1.Rt_Dt
        End If
        cDV_Outlet = cDT_Outlet.DefaultView
        Me.DGV_Outlet.DataSource = cDV_Outlet
        Me.Lb_Outlet.Text = String.Format("სულ {0} ჩანაწერი", cDT_Outlet.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindDTLMSalIn_Click(sender As Object, e As EventArgs) Handles Bt_FindDTLMSalIn.Click
        sender.Enabled = False
        My_Save_Setting("", "", "Dtp_Find_dtlm1", Me.Dtp_Find_dtlm1.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Dtp_Find_dtlm2", Me.Dtp_Find_dtlm2.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Chk_Find_dtlm", Me.Chk_Find_dtlm.Checked)
        Me.DGV_SalIn.DataSource = ""
        cfli_SalInDetIndx1 = -1
        cDT_SalIn = New DataTable
        cDT_SalIn.Columns.Add("sh1", Type.GetType("System.Boolean"))
        cjs_FindData.Clear()
        cjs_FindData.Add(New FindData With {.[property] = "cust_id",
                                            .[operator] = "=",
                                            .value = M_Cur_cust_id})
        cjs_FindData.Add(New FindData With {.[property] = "dtlm",
                                            .[operator] = ">=",
                                            .value = Me.Dtp_Find_dtlm1.Value.ToString("yyyy-MM-ddTHH:mm:ss")})
        If Chk_Find_dtlm.Checked Then
            cjs_FindData.Add(New FindData With {.[property] = "dtlm",
                                            .[operator] = "<=",
                                            .value = Me.Dtp_Find_dtlm2.Value.ToString("yyyy-MM-ddTHH:mm:ss")})
        End If
        Dim fnd_str1 As String = "{""filter"":{""conditions"":" & JsonConvert.SerializeObject(cjs_FindData) & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadFindData("prx_SalIn", fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_SalIn = rt1.Rt_Dt
        End If
        cDV_SalIn = cDT_SalIn.DefaultView
        Me.DGV_SalIn.DataSource = cDV_SalIn
        Me.Lb_SalIn.Text = String.Format("სულ {0} ჩანაწერი", cDT_SalIn.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindDTLMSalOut_Click(sender As Object, e As EventArgs) Handles Bt_FindDTLMSalOut.Click
        sender.Enabled = False
        My_Save_Setting("", "", "Dtp_Find_dtlm1", Me.Dtp_Find_dtlm1.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Dtp_Find_dtlm2", Me.Dtp_Find_dtlm2.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Chk_Find_dtlm", Me.Chk_Find_dtlm.Checked)
        Me.Dgv_SalOut.DataSource = ""
        cfli_SalOutDetIndx1 = -1
        cDT_SalOut = New DataTable
        cDT_SalOut.Columns.Add("sh1", Type.GetType("System.Boolean"))
        cjs_FindData.Clear()
        cjs_FindData.Add(New FindData With {.[property] = "cust_id",
                                            .[operator] = "=",
                                            .value = M_Cur_cust_id})
        cjs_FindData.Add(New FindData With {.[property] = "dtlm",
                                            .[operator] = ">=",
                                            .value = Me.Dtp_Find_dtlm1.Value.ToString("yyyy-MM-ddTHH:mm:ss")})
        If Chk_Find_dtlm.Checked Then
            cjs_FindData.Add(New FindData With {.[property] = "dtlm",
                                            .[operator] = "<=",
                                            .value = Me.Dtp_Find_dtlm2.Value.ToString("yyyy-MM-ddTHH:mm:ss")})
        End If
        Dim fnd_str1 As String = "{""filter"":{""conditions"":" & JsonConvert.SerializeObject(cjs_FindData) & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadFindData("prx_SalOut", fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_SalOut = rt1.Rt_Dt
        End If
        cDV_SalOut = cDT_SalOut.DefaultView
        Me.Dgv_SalOut.DataSource = cDV_SalOut
        Me.Lb_SalOut.Text = String.Format("სულ {0} ჩანაწერი", cDT_SalOut.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindDTLMArchivedLocalStock_Click(sender As Object, e As EventArgs) Handles Bt_FindDTLMArchivedLocalStock.Click
        sender.Enabled = False
        My_Save_Setting("", "", "Dtp_Find_dtlm1", Me.Dtp_Find_dtlm1.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Dtp_Find_dtlm2", Me.Dtp_Find_dtlm2.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Chk_Find_dtlm", Me.Chk_Find_dtlm.Checked)
        Me.DGV_ArchivedLocalStock.DataSource = ""
        cDT_ArchivedLocalStock = New DataTable
        cDT_ArchivedLocalStock.Columns.Add("sh1", Type.GetType("System.Boolean"))
        cjs_FindData.Clear()
        cjs_FindData.Add(New FindData With {.[property] = "cust_id",
                                            .[operator] = "=",
                                            .value = M_Cur_cust_id})
        cjs_FindData.Add(New FindData With {.[property] = "dtlm",
                                            .[operator] = ">=",
                                            .value = Me.Dtp_Find_dtlm1.Value.ToString("yyyy-MM-ddTHH:mm:ss")})
        If Chk_Find_dtlm.Checked Then
            cjs_FindData.Add(New FindData With {.[property] = "dtlm",
                                            .[operator] = "<=",
                                            .value = Me.Dtp_Find_dtlm2.Value.ToString("yyyy-MM-ddTHH:mm:ss")})
        End If
        Dim fnd_str1 As String = "{""filter"":{""conditions"":" & JsonConvert.SerializeObject(cjs_FindData) & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadFindData("prx_ArchivedLocalStock", fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_ArchivedLocalStock = rt1.Rt_Dt
        End If
        cDV_ArchivedLocalStock = cDT_ArchivedLocalStock.DefaultView
        Me.DGV_ArchivedLocalStock.DataSource = cDV_ArchivedLocalStock
        Me.Lb_ArchivedLocalStock.Text = String.Format("სულ {0} ჩანაწერი", cDT_ArchivedLocalStock.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindDTLMOrder_Click(sender As Object, e As EventArgs) Handles Bt_FindDTLMOrder.Click
        sender.Enabled = False
        My_Save_Setting("", "", "Dtp_Find_dtlm1", Me.Dtp_Find_dtlm1.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Dtp_Find_dtlm2", Me.Dtp_Find_dtlm2.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Chk_Find_dtlm", Me.Chk_Find_dtlm.Checked)
        Me.Dgv_Order.DataSource = ""
        cfli_OrderDetIndx1 = -1
        cDT_Order = New DataTable
        cDT_Order.Columns.Add("sh1", Type.GetType("System.Boolean"))
        cjs_FindData.Clear()
        cjs_FindData.Add(New FindData With {.[property] = "cust_id",
                                            .[operator] = "=",
                                            .value = M_Cur_cust_id})
        cjs_FindData.Add(New FindData With {.[property] = "dtlm",
                                            .[operator] = ">=",
                                            .value = Me.Dtp_Find_dtlm1.Value.ToString("yyyy-MM-ddTHH:mm:ss")})
        If Chk_Find_dtlm.Checked Then
            cjs_FindData.Add(New FindData With {.[property] = "dtlm",
                                            .[operator] = "<=",
                                            .value = Me.Dtp_Find_dtlm2.Value.ToString("yyyy-MM-ddTHH:mm:ss")})
        End If
        Dim fnd_str1 As String = "{""filter"":{""conditions"":" & JsonConvert.SerializeObject(cjs_FindData) & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadFindData("prx_Order", fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_Order = rt1.Rt_Dt
        End If
        cDV_Order = cDT_Order.DefaultView
        Me.Dgv_Order.DataSource = cDV_Order
        Me.Lb_Order.Text = String.Format("სულ {0} ჩანაწერი", cDT_Order.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindDTLMOutletDebt_Click(sender As Object, e As EventArgs) Handles Bt_FindDTLMOutletDebt.Click
        sender.Enabled = False
        My_Save_Setting("", "", "Dtp_Find_dtlm1", Me.Dtp_Find_dtlm1.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Dtp_Find_dtlm2", Me.Dtp_Find_dtlm2.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Chk_Find_dtlm", Me.Chk_Find_dtlm.Checked)
        Me.Dgv_OutletDebt.DataSource = ""
        cfli_OutletDebtDetIndx1 = -1
        cDT_OutletDebt = New DataTable
        cDT_OutletDebt.Columns.Add("sh1", Type.GetType("System.Boolean"))
        cjs_FindData.Clear()
        cjs_FindData.Add(New FindData With {.[property] = "cust_id",
                                            .[operator] = "=",
                                            .value = M_Cur_cust_id})
        cjs_FindData.Add(New FindData With {.[property] = "dtlm",
                                            .[operator] = ">=",
                                            .value = Me.Dtp_Find_dtlm1.Value.ToString("yyyy-MM-ddTHH:mm:ss")})
        If Chk_Find_dtlm.Checked Then
            cjs_FindData.Add(New FindData With {.[property] = "dtlm",
                                            .[operator] = "<=",
                                            .value = Me.Dtp_Find_dtlm2.Value.ToString("yyyy-MM-ddTHH:mm:ss")})
        End If
        Dim fnd_str1 As String = "{""filter"":{""conditions"":" & JsonConvert.SerializeObject(cjs_FindData) & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadFindData("prx_OutletDebt", fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_OutletDebt = rt1.Rt_Dt
        End If
        cDV_OutletDebt = cDT_OutletDebt.DefaultView
        Me.Dgv_OutletDebt.DataSource = cDV_OutletDebt
        Me.Lb_OutletDebt.Text = String.Format("სულ {0} ჩანაწერი", cDT_OutletDebt.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Function Fn_DownloadFindData(Pr_FuncName As String, Optional Pr_VelName As String = "date") As Task(Of Rt_Class)
        cjs_FindData.Clear()
        cjs_FindData.Add(New FindData With {.[property] = "cust_id",
                                            .[operator] = "=",
                                            .value = M_Cur_cust_id})
        If Pr_FuncName = "prx_OutletDebt" Then
            If Me.Chk_dt2.Checked Then
                cjs_FindData.Add(New FindData With {.[property] = Pr_VelName,
                                            .[operator] = ">=",
                                            .value = Me.Dtp_01.Value.ToString("yyyy-MM-ddT00:00:00")})
                cjs_FindData.Add(New FindData With {.[property] = Pr_VelName,
                                            .[operator] = "<=",
                                            .value = Me.Dtp_02.Value.ToString("yyyy-MM-ddT00:00:00")})
            Else
                cjs_FindData.Add(New FindData With {.[property] = Pr_VelName,
                                                   .[operator] = ">=",
                                                   .value = Me.Dtp_01.Value.ToString("yyyy-MM-ddT00:00:00")})
                cjs_FindData.Add(New FindData With {.[property] = Pr_VelName,
                                            .[operator] = "<",
                                            .value = Me.Dtp_01.Value.AddDays(1).ToString("yyyy-MM-ddT00:00:00")})
            End If
            Dim fnd_str1 As String = "{""filter"":{""conditions"":" & JsonConvert.SerializeObject(cjs_FindData) & "}}"

            Return Await JS_DownloadFindData(Pr_FuncName, fnd_str1)
        Else
            If Me.Chk_dt2.Checked Then
                cjs_FindData.Add(New FindData With {.[property] = Pr_VelName,
                                                .[operator] = ">=",
                                                .value = Me.Dtp_01.Value.ToString("yyyy-MM-dd")})
                cjs_FindData.Add(New FindData With {.[property] = Pr_VelName,
                                                .[operator] = "<=",
                                                .value = Me.Dtp_02.Value.ToString("yyyy-MM-dd")})
            Else
                cjs_FindData.Add(New FindData With {.[property] = Pr_VelName,
                                                       .[operator] = ">=",
                                                       .value = Me.Dtp_01.Value.ToString("yyyy-MM-dd")})
                cjs_FindData.Add(New FindData With {.[property] = Pr_VelName,
                                                .[operator] = "<",
                                                .value = Me.Dtp_01.Value.AddDays(1).ToString("yyyy-MM-dd")})
            End If
            Dim fnd_str1 As String = "{""filter"":{""conditions"":" & JsonConvert.SerializeObject(cjs_FindData) & "}}"

            Return Await JS_DownloadFindData(Pr_FuncName, fnd_str1)
        End If


    End Function

    Private Async Sub Bt_FindDateSalIn_Click(sender As Object, e As EventArgs) Handles Bt_FindDateSalIn.Click
        sender.Enabled = False
        My_Save_Setting("", "", "Dtp_01", Me.Dtp_01.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Dtp_02", Me.Dtp_02.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Chk_dt2", Me.Chk_dt2.Checked)
        Me.DGV_SalIn.DataSource = ""
        cfli_SalInDetIndx1 = -1
        cDT_SalIn = New DataTable
        cDT_SalIn.Columns.Add("sh1", Type.GetType("System.Boolean"))
        Dim rt1 As Rt_Class = Await Fn_DownloadFindData("prx_SalIn")
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_SalIn = rt1.Rt_Dt
        End If
        cDV_SalIn = cDT_SalIn.DefaultView
        Me.DGV_SalIn.DataSource = cDV_SalIn
        Me.Lb_SalIn.Text = String.Format("სულ {0} ჩანაწერი", cDT_SalIn.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindDateSalOut_Click(sender As Object, e As EventArgs) Handles Bt_FindDateSalOut.Click
        sender.Enabled = False
        My_Save_Setting("", "", "Dtp_01", Me.Dtp_01.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Dtp_02", Me.Dtp_02.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Chk_dt2", Me.Chk_dt2.Checked)
        Me.Dgv_SalOut.DataSource = ""
        cfli_SalOutDetIndx1 = -1
        cDT_SalOut = New DataTable
        cDT_SalOut.Columns.Add("sh1", Type.GetType("System.Boolean"))
        Dim rt1 As Rt_Class = Await Fn_DownloadFindData("prx_SalOut")
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_SalOut = rt1.Rt_Dt
        End If
        cDV_SalOut = cDT_SalOut.DefaultView
        Me.Dgv_SalOut.DataSource = cDV_SalOut
        Me.Lb_SalOut.Text = String.Format("სულ {0} ჩანაწერი", cDT_SalOut.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindDateArchivedLocalStock_Click(sender As Object, e As EventArgs) Handles Bt_FindDateArchivedLocalStock.Click
        sender.Enabled = False
        My_Save_Setting("", "", "Dtp_01", Me.Dtp_01.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Dtp_02", Me.Dtp_02.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Chk_dt2", Me.Chk_dt2.Checked)
        Me.DGV_ArchivedLocalStock.DataSource = ""
        cDT_ArchivedLocalStock = New DataTable
        cDT_ArchivedLocalStock.Columns.Add("sh1", Type.GetType("System.Boolean"))
        Dim rt1 As Rt_Class = Await Fn_DownloadFindData("prx_ArchivedLocalStock")
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_ArchivedLocalStock = rt1.Rt_Dt
        End If
        cDV_ArchivedLocalStock = cDT_ArchivedLocalStock.DefaultView
        Me.DGV_ArchivedLocalStock.DataSource = cDV_ArchivedLocalStock
        Me.Lb_ArchivedLocalStock.Text = String.Format("სულ {0} ჩანაწერი", cDT_ArchivedLocalStock.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindDateOrder_Click(sender As Object, e As EventArgs) Handles Bt_FindDateOrder.Click
        sender.Enabled = False
        My_Save_Setting("", "", "Dtp_01", Me.Dtp_01.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Dtp_02", Me.Dtp_02.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Chk_dt2", Me.Chk_dt2.Checked)
        Me.Dgv_Order.DataSource = ""
        cfli_OrderDetIndx1 = -1
        cDT_Order = New DataTable
        cDT_Order.Columns.Add("sh1", Type.GetType("System.Boolean"))
        Dim rt1 As Rt_Class = Await Fn_DownloadFindData("prx_Order")
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_Order = rt1.Rt_Dt
        End If
        cDV_Order = cDT_Order.DefaultView
        Me.Dgv_Order.DataSource = cDV_Order
        Me.Lb_Order.Text = String.Format("სულ {0} ჩანაწერი", cDT_Order.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_FindDate_calcOutletDebt_Click(sender As Object, e As EventArgs) Handles Bt_FindDate_calcOutletDebt.Click
        sender.Enabled = False
        My_Save_Setting("", "", "Dtp_01", Me.Dtp_01.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Dtp_02", Me.Dtp_02.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Chk_dt2", Me.Chk_dt2.Checked)
        Me.Dgv_OutletDebt.DataSource = ""
        cfli_OutletDebtDetIndx1 = -1
        cDT_OutletDebt = New DataTable
        cDT_OutletDebt.Columns.Add("sh1", Type.GetType("System.Boolean"))

        Dim rt1 As Rt_Class = Await Fn_DownloadFindData("prx_OutletDebt", "date_calc")
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_OutletDebt = rt1.Rt_Dt
        End If
        cDV_OutletDebt = cDT_OutletDebt.DefaultView
        Me.Dgv_OutletDebt.DataSource = cDV_OutletDebt
        Me.Lb_OutletDebt.Text = String.Format("სულ {0} ჩანაწერი", cDT_OutletDebt.Rows.Count)
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_DelParentCompany_Click(sender As Object, e As EventArgs) Handles Bt_DelParentCompany.Click
        sender.Enabled = False
        Try
            Dim rt_01 As Boolean = False
            Dim indx1 As Integer = 0
            Dim rw_rd1 As Integer = Me.DGV_ParentCompany.Rows.Count
            For Each DtRw1 As DataRowView In cDV_ParentCompany
                Await Task.Delay(50)
                indx1 += 1
                UpdateProgressBar1(indx1 * 100 / rw_rd1)
                If DtRw1("sh1") Then
                    Dim cr_id As String = DtRw1("id")
                    Dim rt1 As Rt_Class = Await JS_DeleteCurentID("prx_ParentCompany", cr_id)
                    If Not rt1.Rt_flg Then
                        MsgBox(rt1,, "")
                    Else
                        rt_01 = True
                    End If
                End If
            Next
            If rt_01 Then
                Me.DGV_ParentCompany.DataSource = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message,, "")
        End Try
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_DelOutlet_Click(sender As Object, e As EventArgs) Handles Bt_DelOutlet.Click
        sender.Enabled = False
        Try
            Dim rw_rd1 As Integer = cDT_Outlet.Rows.Count
            For irw1 As Integer = rw_rd1 - 1 To 0 Step -1
                Dim DtRw1 As DataRow = cDT_Outlet.Rows(irw1)
                UpdateProgressBar1((irw1 + 1) * 100 / rw_rd1)
                Await Task.Delay(50)
                If DtRw1("sh1") Then
                    Dim cr_id As String = DtRw1("id")
                    Dim rt1 As Rt_Class = Await JS_DeleteCurentID("prx_Outlet", cr_id)
                    If Not rt1.Rt_flg Then
                        MsgBox(rt1,, "")
                        Exit Sub
                    Else
                        cDT_Outlet.Rows.Remove(DtRw1)
                    End If
                End If
            Next
            Me.DGV_Outlet.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message,, "")
        End Try
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_DelSalIn_Click(sender As Object, e As EventArgs) Handles Bt_DelSalIn.Click
        sender.Enabled = False
        Try
            Dim rw_rd1 As Integer = cDT_SalIn.Rows.Count
            For irw1 As Integer = rw_rd1 - 1 To 0 Step -1
                Dim DtRw1 As DataRow = cDT_SalIn.Rows(irw1)
                UpdateProgressBar1((irw1 + 1) * 100 / rw_rd1)
                Await Task.Delay(50)
                If DtRw1("sh1") Then
                    Dim cr_id As String = DtRw1("id")
                    Dim rt1 As Rt_Class = Await JS_DeleteCurentID("prx_SalIn", cr_id)
                    If Not rt1.Rt_flg Then
                        MsgBox(rt1,, "")
                        Exit Sub
                    Else
                        cDT_SalIn.Rows.Remove(DtRw1)
                    End If
                End If
            Next
            Me.DGV_SalIn.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message,, "")
        End Try
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_DelSalOut_Click(sender As Object, e As EventArgs) Handles Bt_DelSalOut.Click
        sender.Enabled = False
        Try
            Dim rw_rd1 As Integer = cDT_SalOut.Rows.Count
            For irw1 As Integer = rw_rd1 - 1 To 0 Step -1
                Dim DtRw1 As DataRow = cDT_SalOut.Rows(irw1)
                UpdateProgressBar1((irw1 + 1) * 100 / rw_rd1)
                Await Task.Delay(50)
                If DtRw1("sh1") Then
                    Dim cr_id As String = DtRw1("id")
                    Dim rt1 As Rt_Class = Await JS_DeleteCurentID("prx_SalOut", cr_id)
                    If Not rt1.Rt_flg Then
                        MsgBox(rt1,, "")
                        Exit Sub
                    Else
                        cDT_SalOut.Rows.Remove(DtRw1)
                    End If
                End If
            Next
            Me.Dgv_SalOut.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message,, "")
        End Try
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_DelArchivedLocalStock_Click(sender As Object, e As EventArgs) Handles Bt_DelArchivedLocalStock.Click
        sender.Enabled = False
        Try
            Dim rw_rd1 As Integer = cDT_ArchivedLocalStock.Rows.Count
            For irw1 As Integer = rw_rd1 - 1 To 0 Step -1
                Dim DtRw1 As DataRow = cDT_ArchivedLocalStock.Rows(irw1)
                UpdateProgressBar1((irw1 + 1) * 100 / rw_rd1)
                Await Task.Delay(50)
                If DtRw1("sh1") Then
                    Dim cr_id As String = DtRw1("id")
                    Dim rt1 As Rt_Class = Await JS_DeleteCurentID("prx_ArchivedLocalStock", cr_id)
                    If Not rt1.Rt_flg Then
                        MsgBox(rt1,, "")
                        Exit Sub
                    Else
                        cDT_ArchivedLocalStock.Rows.Remove(DtRw1)
                    End If
                End If
            Next
            Me.DGV_ArchivedLocalStock.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message,, "")
        End Try
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_DelOrder_Click(sender As Object, e As EventArgs) Handles Bt_DelOrder.Click
        sender.Enabled = False
        Try
            Dim rw_rd1 As Integer = cDT_Order.Rows.Count
            For irw1 As Integer = rw_rd1 - 1 To 0 Step -1
                Dim DtRw1 As DataRow = cDT_Order.Rows(irw1)
                UpdateProgressBar1((irw1 + 1) * 100 / rw_rd1)
                Await Task.Delay(50)
                If DtRw1("sh1") Then
                    Dim cr_id As String = DtRw1("id")
                    Dim rt1 As Rt_Class = Await JS_DeleteCurentID("prx_Order", cr_id)
                    If Not rt1.Rt_flg Then
                        MsgBox(rt1,, "")
                        Exit Sub
                    Else
                        cDT_Order.Rows.Remove(DtRw1)
                    End If
                End If
            Next
            Me.Dgv_Order.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message,, "")
        End Try
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_DelOutletDebt_Click(sender As Object, e As EventArgs) Handles Bt_DelOutletDebt.Click
        sender.Enabled = False
        Try
            Dim rw_rd1 As Integer = cDT_OutletDebt.Rows.Count
            For irw1 As Integer = rw_rd1 - 1 To 0 Step -1
                Dim DtRw1 As DataRow = cDT_OutletDebt.Rows(irw1)
                UpdateProgressBar1((irw1 + 1) * 100 / rw_rd1)
                Await Task.Delay(50)
                If DtRw1("sh1") Then
                    Dim cr_id As String = DtRw1("id")
                    Dim rt1 As Rt_Class = Await JS_DeleteCurentID("prx_OutletDebt", cr_id)
                    If Not rt1.Rt_flg Then
                        MsgBox(rt1,, "")
                        Exit Sub
                    Else
                        cDT_OutletDebt.Rows.Remove(DtRw1)
                    End If
                End If
            Next
            Me.Dgv_OutletDebt.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message,, "")
        End Try
        sender.Enabled = True
    End Sub

    Private Sub Tb_FltrLocalProduct_TextChanged(sender As Object, e As EventArgs) Handles Tb_FltrLocalProduct.TextChanged
        Fnd_LocalProduct()
    End Sub

    Private Sub Fnd_LocalProduct()
        Dim flt_txt1 As String = ""
        Dim flt_and As String = ""
        If Me.Tb_FltrLocalProduct.Text.Trim.Length > 0 Then
            Dim txtar1() As String = Me.Tb_FltrLocalProduct.Text.Split(" ")
            For Each txt_t1 As String In txtar1
                flt_txt1 &= flt_and & " ((localcode like '%" & txt_t1.Trim & "%') OR (code like '%" & txt_t1.Trim & "%') OR (name like '%" & txt_t1.Trim & "%') OR (shortname like '%" & txt_t1.Trim & "%'))"
                flt_and = " AND "
            Next
        End If
        cDV_LocalProduct.RowFilter = flt_txt1
    End Sub

    Private Sub Tb_FltrParentCompany_TextChanged(sender As Object, e As EventArgs) Handles Tb_FltrParentCompany.TextChanged
        Fnd_ParentCompany()
    End Sub

    Private Sub Fnd_ParentCompany()
        Dim flt_txt1 As String = ""
        Dim flt_and As String = ""
        If Me.Tb_FltrParentCompany.Text.Trim.Length > 0 Then
            Dim txtar1() As String = Me.Tb_FltrParentCompany.Text.Split(" ")
            For Each txt_t1 As String In txtar1
                flt_txt1 &= flt_and & " ((pcomp_code like '%" & txt_t1.Trim & "%') OR (pc_name like '%" & txt_t1.Trim & "%'))"
                flt_and = " AND "
            Next
        End If
        cDV_ParentCompany.RowFilter = flt_txt1
    End Sub

    Private Sub Tb_FltrOutlets_TextChanged(sender As Object, e As EventArgs) Handles Tb_FltrOutlets.TextChanged
        Fnd_Outlets()
    End Sub

    Private Sub Fnd_Outlets()
        Dim flt_txt1 As String = ""
        Dim flt_and As String = ""
        If Me.Tb_FltrOutlets.Text.Trim.Length > 0 Then
            Dim txtar1() As String = Me.Tb_FltrOutlets.Text.Split(" ")
            For Each txt_t1 As String In txtar1
                flt_txt1 &= flt_and & " ((ipn like '%" & txt_t1.Trim & "%') OR (name like '%" & txt_t1.Trim & "%') OR (trade_name like '%" & txt_t1.Trim & "%') OR (ol_code like '%" & txt_t1.Trim & "%'))"
                flt_and = " AND "
            Next
        End If
        cDV_Outlet.RowFilter = flt_txt1
    End Sub

    Private Sub Tb_FltrSalIn_TextChanged(sender As Object, e As EventArgs) Handles Tb_FltrSalIn.TextChanged
        Fnd_SalIn()
    End Sub

    Private Sub Fnd_SalIn()
        Dim flt_txt1 As String = ""
        Dim flt_and As String = ""
        If Me.Tb_FltrSalIn.Text.Trim.Length > 0 Then
            Dim txtar1() As String = Me.Tb_FltrSalIn.Text.Split(" ")
            For Each txt_t1 As String In txtar1
                flt_txt1 &= flt_and & " ((invoice_no like '%" & txt_t1.Trim & "%') OR (custdoc_no like '%" & txt_t1.Trim & "%') OR (wareh_code like '%" & txt_t1.Trim & "%'))"
                flt_and = " AND "
            Next
        End If
        cDV_SalIn.RowFilter = flt_txt1
    End Sub

    Private Sub Tb_FiltreSalOut_TextChanged(sender As Object, e As EventArgs) Handles Tb_FltrSalOut.TextChanged
        Fnd_SalOut()
    End Sub

    Private Sub Fnd_SalOut()
        Dim flt_txt1 As String = ""
        Dim flt_and As String = ""
        If Me.Tb_FltrSalOut.Text.Trim.Length > 0 Then
            Dim txtar1() As String = Me.Tb_FltrSalOut.Text.Split(" ")
            For Each txt_t1 As String In txtar1
                Dim txt_i1 As Integer
                If Not Integer.TryParse(txt_t1, txt_i1) Then
                    txt_i1 = 0
                End If
                flt_txt1 &= flt_and & $" ((invoice_no like '%{txt_t1}%') OR (cinvoic_no like '%{txt_t1}%') OR (merch_id={txt_i1}) OR (wareh_code like '%{txt_t1}%'))"
                flt_and = " AND "
            Next
        End If
        cDV_SalOut.RowFilter = flt_txt1
    End Sub

    Private Sub Tb_FltrArchivedLocalStock_TextChanged(sender As Object, e As EventArgs) Handles Tb_FltrArchivedLocalStock.TextChanged
        Fnd_ArchivedLocalStock()
    End Sub

    Private Sub Fnd_ArchivedLocalStock()
        Dim flt_txt1 As String = ""
        Dim flt_and As String = ""
        If Me.Tb_FltrArchivedLocalStock.Text.Trim.Length > 0 Then
            Dim txtar1() As String = Me.Tb_FltrArchivedLocalStock.Text.Split(" ")
            For Each txt_t1 As String In txtar1
                flt_txt1 &= flt_and & " ((localcode like '%" & txt_t1.Trim & "%') OR (id like '%" & txt_t1.Trim & "%'))"
                flt_and = " AND "
            Next
        End If
        cDV_ArchivedLocalStock.RowFilter = flt_txt1
    End Sub

    Private Sub Tb_FiltreOrder_TextChanged(sender As Object, e As EventArgs) Handles Tb_FltrOrder.TextChanged
        Fnd_Order()
    End Sub

    Private Sub Fnd_Order()
        Dim flt_txt1 As String = ""
        Dim flt_and As String = ""
        If Me.Tb_FltrOrder.Text.Trim.Length > 0 Then
            Dim txtar1() As String = Me.Tb_FltrOrder.Text.Split(" ")
            For Each txt_t1 As String In txtar1
                Dim txt_i1 As Integer
                If Not Integer.TryParse(txt_t1, txt_i1) Then
                    txt_i1 = 0
                End If
                flt_txt1 &= flt_and & $" ((invoice_no like '%{txt_t1}%') OR (cinvoic_no like '%{txt_t1}%') OR (merch_id={txt_i1}) OR (wareh_code like '%{txt_t1}%'))"
                flt_and = " AND "
            Next
        End If
        cDV_Order.RowFilter = flt_txt1
    End Sub

    Private Sub Tb_FiltreOutletDebt_TextChanged(sender As Object, e As EventArgs) Handles Tb_FltrOutletDebt.TextChanged
        Fnd_OutletDebt()
    End Sub

    Private Sub Fnd_OutletDebt()
        Dim flt_txt1 As String = ""
        Dim flt_and As String = ""
        If Me.Tb_FltrOutletDebt.Text.Trim.Length > 0 Then
            Dim txtar1() As String = Me.Tb_FltrOutletDebt.Text.Split(" ")
            For Each txt_t1 As String In txtar1
                Dim txt_i1 As Integer
                If Not Integer.TryParse(txt_t1, txt_i1) Then
                    txt_i1 = 0
                End If
                flt_txt1 &= flt_and & $" ((invoice_no like '%{txt_t1}%') OR (cinvoic_no like '%{txt_t1}%') OR (merch_id={txt_i1}) OR (wareh_code like '%{txt_t1}%'))"
                flt_and = " AND "
            Next
        End If
        cDV_OutletDebt.RowFilter = flt_txt1
    End Sub

    Private Async Sub Bt_CheckSumSalIn_Click(sender As Object, e As EventArgs) Handles Bt_CheckSumSalIn.Click
        sender.Enabled = False
        My_Save_Setting("", "", "Dtp_CheckSum1", Me.Dtp_CheckSum1.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Dtp_CheckSum2", Me.Dtp_CheckSum2.Value.ToString("yyyy-MM-dd"))
        Dim dt_txt1 As String = Me.Dtp_CheckSum1.Value.ToString("yyyy-MM-dd")
        Dim dt_txt2 As String = Me.Dtp_CheckSum2.Value.ToString("yyyy-MM-dd")
        Me.DGV_CheckSum.DataSource = ""
        cDT_CheckSumSalIn = New DataTable

        Dim fnd_str1 As String = $"""startDate"": ""{dt_txt1}"",""endDate"" : ""{dt_txt2}"",""cust_id"" : {M_Cur_cust_id},""tableName"" : ""SalIn"""
        fnd_str1 = "{""param"" : {" & fnd_str1 & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadCheckSumData(fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_CheckSumSalIn = rt1.Rt_Dt
        End If
        cDV_CheckSumSalIn = cDT_CheckSumSalIn.DefaultView
        Me.DGV_CheckSum.DataSource = cDV_CheckSumSalIn
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_CheckSumSalOut_Click(sender As Object, e As EventArgs) Handles Bt_CheckSumSalOut.Click
        sender.Enabled = False
        My_Save_Setting("", "", "Dtp_CheckSum1", Me.Dtp_CheckSum1.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Dtp_CheckSum2", Me.Dtp_CheckSum2.Value.ToString("yyyy-MM-dd"))
        Dim dt_txt1 As String = Me.Dtp_CheckSum1.Value.ToString("yyyy-MM-dd")
        Dim dt_txt2 As String = Me.Dtp_CheckSum2.Value.ToString("yyyy-MM-dd")
        Me.DGV_CheckSum.DataSource = ""
        cDT_CheckSumSalOut = New DataTable

        Dim fnd_str1 As String = $"""startDate"": ""{dt_txt1}"",""endDate"" : ""{dt_txt2}"",""cust_id"" : {M_Cur_cust_id},""tableName"" : ""SalOut"""
        fnd_str1 = "{""param"" : {" & fnd_str1 & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadCheckSumData(fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_CheckSumSalOut = rt1.Rt_Dt
        End If
        cDV_CheckSumSalOut = cDT_CheckSumSalOut.DefaultView
        Me.DGV_CheckSum.DataSource = cDV_CheckSumSalOut
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_CheckSumOrder_Click(sender As Object, e As EventArgs) Handles Bt_CheckSumOrder.Click
        sender.Enabled = False
        My_Save_Setting("", "", "Dtp_CheckSum1", Me.Dtp_CheckSum1.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Dtp_CheckSum2", Me.Dtp_CheckSum2.Value.ToString("yyyy-MM-dd"))
        Dim dt_txt1 As String = Me.Dtp_CheckSum1.Value.ToString("yyyy-MM-dd")
        Dim dt_txt2 As String = Me.Dtp_CheckSum2.Value.ToString("yyyy-MM-dd")
        Me.DGV_CheckSum.DataSource = ""
        cDT_CheckSumOrder = New DataTable

        Dim fnd_str1 As String = $"""startDate"": ""{dt_txt1}"",""endDate"" : ""{dt_txt2}"",""cust_id"" : {M_Cur_cust_id},""tableName"" : ""Order"""
        fnd_str1 = "{""param"" : {" & fnd_str1 & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadCheckSumData(fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_CheckSumOrder = rt1.Rt_Dt
        End If
        cDV_CheckSumOrder = cDT_CheckSumOrder.DefaultView
        Me.DGV_CheckSum.DataSource = cDV_CheckSumOrder
        sender.Enabled = True
    End Sub

    Private Async Sub Bt_CheckSumOutletDebt_Click(sender As Object, e As EventArgs) Handles Bt_CheckSumOutletDebt.Click
        sender.Enabled = False
        My_Save_Setting("", "", "Dtp_CheckSum1", Me.Dtp_CheckSum1.Value.ToString("yyyy-MM-dd"))
        My_Save_Setting("", "", "Dtp_CheckSum2", Me.Dtp_CheckSum2.Value.ToString("yyyy-MM-dd"))
        Dim dt_txt1 As String = Me.Dtp_CheckSum1.Value.ToString("yyyy-MM-dd")
        Dim dt_txt2 As String = Me.Dtp_CheckSum2.Value.ToString("yyyy-MM-dd")
        Me.DGV_CheckSum.DataSource = ""
        cDT_CheckSumOutletDebt = New DataTable

        Dim fnd_str1 As String = $"""startDate"": ""{dt_txt1}"",""endDate"" : ""{dt_txt2}"",""cust_id"" : {M_Cur_cust_id},""tableName"" : ""OutletDebt"""
        fnd_str1 = "{""param"" : {" & fnd_str1 & "}}"

        Dim rt1 As Rt_Class = Await JS_DownloadCheckSumData(fnd_str1)
        If Not rt1.Rt_flg Then
            MsgBox(rt1.Rt_txt,, "")
        Else
            cDT_CheckSumOutletDebt = rt1.Rt_Dt
        End If
        cDV_CheckSumOutletDebt = cDT_CheckSumOutletDebt.DefaultView
        Me.DGV_CheckSum.DataSource = cDV_CheckSumOutletDebt
        sender.Enabled = True
    End Sub

    Private Sub DGV_CheckSum_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV_CheckSum.CellMouseDown
        cfli_indCheckSum1 = e.RowIndex
    End Sub

    Private Async Function Fn_DownloadCurData(Pr_FuncName As String, Pr_date1 As DateTime, Optional Pr_VelName As String = "date") As Task(Of Rt_Class)
        cjs_FindData.Clear()
        cjs_FindData.Add(New FindData With {.[property] = "cust_id",
                                        .[operator] = "=",
                                        .value = M_Cur_cust_id})
        If Pr_FuncName = "prx_OutletDebt" Then
            cjs_FindData.Add(New FindData With {.[property] = Pr_VelName,
                                        .[operator] = ">=",
                                        .value = Pr_date1.ToString("yyyy-MM-ddT00:00:00")})
            cjs_FindData.Add(New FindData With {.[property] = Pr_VelName,
                                        .[operator] = "<",
                                        .value = Pr_date1.AddDays(1).ToString("yyyy-MM-ddT00:00:00")})
        Else
            cjs_FindData.Add(New FindData With {.[property] = Pr_VelName,
                                        .[operator] = ">=",
                                        .value = Pr_date1.ToString("yyyy-MM-dd")})
            cjs_FindData.Add(New FindData With {.[property] = Pr_VelName,
                                        .[operator] = "<",
                                        .value = Pr_date1.AddDays(1).ToString("yyyy-MM-dd")})
        End If

        Dim fnd_str1 As String = "{""filter"":{""conditions"":" & JsonConvert.SerializeObject(cjs_FindData) & "}}"

        Return Await JS_DownloadFindData(Pr_FuncName, fnd_str1)
    End Function

    Private Async Sub ViewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewToolStripMenuItem.Click
        If cfli_indCheckSum1 >= 0 Then
            Dim cur_date1 As DateTime = CDate(Me.DGV_CheckSum.Rows(cfli_indCheckSum1).Cells("sumDate").Value)
            Select Case Me.DGV_CheckSum.Rows(cfli_indCheckSum1).Cells("tableName").Value.ToString.ToLower
                Case "salin"
                    TabCont1.SelectedTab = TP_SalIn
                    Me.DGV_SalIn.DataSource = ""
                    cfli_SalInDetIndx1 = -1
                    cDT_SalIn = New DataTable
                    Dim rt1 As Rt_Class = Await Fn_DownloadCurData("prx_SalIn", cur_date1)
                    If Not rt1.Rt_flg Then
                        MsgBox(rt1.Rt_txt,, "")
                    Else
                        cDT_SalIn = rt1.Rt_Dt
                    End If
                    cDT_SalIn.Columns.Add("sh1", Type.GetType("System.Boolean"))
                    cDV_SalIn = cDT_SalIn.DefaultView
                    Me.DGV_SalIn.DataSource = cDV_SalIn
                    Me.Lb_SalIn.Text = String.Format("სულ {0} ჩანაწერი", cDT_SalIn.Rows.Count)
                Case "salout"
                    TabCont1.SelectedTab = TP_SalOut
                    Me.Dgv_SalOut.DataSource = ""
                    cfli_SalOutDetIndx1 = -1
                    cDT_SalOut = New DataTable
                    Dim rt1 As Rt_Class = Await Fn_DownloadCurData("prx_SalOut", cur_date1)
                    If Not rt1.Rt_flg Then
                        MsgBox(rt1.Rt_txt,, "")
                    Else
                        cDT_SalOut = rt1.Rt_Dt
                    End If
                    cDT_SalOut.Columns.Add("sh1", Type.GetType("System.Boolean"))
                    cDV_SalOut = cDT_SalOut.DefaultView
                    Me.Dgv_SalOut.DataSource = cDV_SalOut
                    Me.Lb_SalOut.Text = String.Format("სულ {0} ჩანაწერი", cDT_SalOut.Rows.Count)
                Case "order"
                    TabCont1.SelectedTab = TP_Order
                    Me.Dgv_Order.DataSource = ""
                    cfli_OrderDetIndx1 = -1
                    cDT_Order = New DataTable
                    Dim rt1 As Rt_Class = Await Fn_DownloadCurData("prx_Order", cur_date1)
                    If Not rt1.Rt_flg Then
                        MsgBox(rt1.Rt_txt,, "")
                    Else
                        cDT_Order = rt1.Rt_Dt
                    End If
                    cDT_Order.Columns.Add("sh1", Type.GetType("System.Boolean"))
                    cDV_Order = cDT_Order.DefaultView
                    Me.Dgv_Order.DataSource = cDV_Order
                    Me.Lb_Order.Text = String.Format("სულ {0} ჩანაწერი", cDT_Order.Rows.Count)
                Case "outletdebt"
                    TabCont1.SelectedTab = TP_OutletDebt
                    Me.Dgv_OutletDebt.DataSource = ""
                    cfli_OutletDebtDetIndx1 = -1
                    cDT_OutletDebt = New DataTable
                    Dim rt1 As Rt_Class = Await Fn_DownloadCurData("prx_OutletDebt", cur_date1, "date_calc")
                    If Not rt1.Rt_flg Then
                        MsgBox(rt1.Rt_txt,, "")
                    Else
                        cDT_OutletDebt = rt1.Rt_Dt
                    End If
                    cDT_OutletDebt.Columns.Add("sh1", Type.GetType("System.Boolean"))
                    cDV_OutletDebt = cDT_OutletDebt.DefaultView
                    Me.Dgv_OutletDebt.DataSource = cDV_OutletDebt
                    Me.Lb_OutletDebt.Text = String.Format("სულ {0} ჩანაწერი", cDT_OutletDebt.Rows.Count)
            End Select
        End If
    End Sub

    Private Async Function Load_Detail(Pr_FuncName As String, Pr_ID As String, Pr_Dm_par1 As String, Pr_DetailName As String) As Task(Of DataTable)
        Dim cur_DT1 As New DataTable
        Dim rt_str1 As Rt_Class = Await JS_DownloadCurentID(Pr_FuncName, Pr_ID, Pr_Dm_par1)
        If rt_str1.Rt_flg Then
            Dim jsonToken2 As JToken = JsonConvert.DeserializeObject(Of JToken)(rt_str1.Rt_txt)
            Dim jsonArray1 As JArray = DirectCast(jsonToken2(Pr_DetailName), JArray)
            cur_DT1 = cDT_goods.Clone
            If jsonArray1.Count > 0 Then
                Dim flg_det_id As Boolean = True
                Dim flg_checkId As Boolean = True
                For Each JS_02 As JToken In jsonArray1
                    Dim dr11 As DataRow = cur_DT1.NewRow
                    dr11("id") = JS_02("id")
                    If JS_02("det_id") IsNot Nothing Then
                        dr11("det_id") = JS_02("det_id")
                        flg_det_id = False
                    End If
                    If JS_02("checkId") IsNot Nothing Then
                        dr11("checkid") = JS_02("checkId")
                        flg_checkId = False
                    End If
                    dr11("localcode") = JS_02("localcode")
                    dr11("price") = JS_02("price")
                    dr11("qty") = JS_02("qty")
                    Try
                        Dim JToken_LC As JToken = DirectCast(JS_02("localcode"), JToken)
                        dr11("name") = JToken_LC("name")
                        dr11("localcode") = JToken_LC("localcode")
                    Catch ex As Exception
                    End Try
                    cur_DT1.Rows.Add(dr11)
                Next
                If flg_det_id Then
                    cur_DT1.Columns.Remove("det_id")
                End If
                If flg_checkId Then
                    cur_DT1.Columns.Remove("checkid")
                End If
            End If
        Else
            MsgBox(rt_str1.Rt_txt,, "")
        End If
        Return cur_DT1
    End Function

    Private Async Function Load_DetailOutletDebt(Pr_FuncName As String, Pr_ID As String, Pr_Dm_par1 As String, Pr_DetailName As String) As Task(Of DataTable)
        Dim cur_DT1 As New DataTable
        Dim rt_str1 As Rt_Class = Await JS_DownloadCurentID(Pr_FuncName, Pr_ID, Pr_Dm_par1)
        If rt_str1.Rt_flg Then
            Dim jsonToken2 As JToken = JsonConvert.DeserializeObject(Of JToken)(rt_str1.Rt_txt)
            Dim jsonArray1 As JArray = DirectCast(jsonToken2(Pr_DetailName), JArray)
            cur_DT1 = cDT_OutletDebtDet.Clone
            If jsonArray1.Count > 0 Then
                Dim flg_det_id As Boolean = True
                Dim flg_checkId As Boolean = True
                For Each JS_02 As JToken In jsonArray1
                    Dim dr11 As DataRow = cur_DT1.NewRow
                    dr11("id") = JS_02("id")
                    If JS_02("det_id") IsNot Nothing Then
                        dr11("det_id") = JS_02("det_id")
                        flg_det_id = False
                    End If
                    If JS_02("checkId") IsNot Nothing Then
                        dr11("checkid") = JS_02("checkId")
                        flg_checkId = False
                    End If
                    dr11("date") = JS_02("date")
                    dr11("debt") = JS_02("debt")
                    dr11("qty") = JS_02("qty")
                    dr11("debtypcode") = JS_02("debtypcode")
                    dr11("invoice_no") = JS_02("invoice_no")
                    cur_DT1.Rows.Add(dr11)
                Next
                If flg_det_id Then
                    cur_DT1.Columns.Remove("det_id")
                End If
                If flg_checkId Then
                    cur_DT1.Columns.Remove("checkid")
                End If
            End If
        Else
            MsgBox(rt_str1.Rt_txt,, "")
        End If
        Return cur_DT1
    End Function

    Private Sub DGV_SalIn_SelectionChanged(sender As Object, e As EventArgs) Handles DGV_SalIn.SelectionChanged
        If cflg_DetCamot1 Then
            cflg_DetCamot1 = False
            Try
                If Me.DGV_SalIn.SelectedCells.Count > 0 Then
                    Me.Dgv_SalInDet.DataSource = ""
                    cfli_SalInIndx1 = Me.DGV_SalIn.SelectedCells(0).RowIndex
                    Load_SalInDet()
                End If
            Catch ex As Exception
                Dim a12 = 1
            End Try
            cflg_DetCamot1 = True
        End If
    End Sub

    Private Sub DGV_SalIn_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV_SalIn.CellMouseDown
        cfli_SalInIndx1 = e.RowIndex
        If cflg_DetCamot1 Then
            cflg_DetCamot1 = False
            Load_SalInDet()
            cflg_DetCamot1 = True
        End If
    End Sub

    Private Async Sub Load_SalInDet()
        Try
            If cfli_SalInIndx1 >= 0 Then
                Me.Dgv_SalInDet.DataSource = ""
                cfli_SalInIndx1 = Me.DGV_SalIn.SelectedCells(0).RowIndex
                Dim cur_id1 As String = Me.DGV_SalIn.Rows(cfli_SalInIndx1).Cells("id").Value
                Dim cur_DT_goods As DataTable
                cur_DT_goods = Await Load_Detail("prx_SalIn", cur_id1, "&fetchPlan=salIn-full", "salInLocalDetail")
                Me.Dgv_SalInDet.DataSource = cur_DT_goods
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Dgv_SalOut_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles Dgv_SalOut.CellMouseDown
        cfli_SalOutIndx1 = e.RowIndex
        If cflg_DetCamot1 Then
            cflg_DetCamot1 = False
            Load_SalOutDet()
            cflg_DetCamot1 = True
        End If
    End Sub

    Private Sub Dgv_SalOut_SelectionChanged(sender As Object, e As EventArgs) Handles Dgv_SalOut.SelectionChanged
        If cflg_DetCamot1 Then
            cflg_DetCamot1 = False
            Try
                If Me.Dgv_SalOut.SelectedCells.Count > 0 Then
                    Me.Dgv_SalOutDet.DataSource = ""
                    cfli_SalOutIndx1 = Me.Dgv_SalOut.SelectedCells(0).RowIndex
                    Load_SalOutDet()
                End If
            Catch ex As Exception
            End Try
            cflg_DetCamot1 = True
        End If
    End Sub

    Private Async Sub Load_SalOutDet()
        Try
            If cfli_SalOutIndx1 >= 0 Then
                Me.Dgv_SalOutDet.DataSource = ""
                cfli_SalOutIndx1 = Me.Dgv_SalOut.SelectedCells(0).RowIndex
                Dim cur_id1 As String = Me.Dgv_SalOut.Rows(cfli_SalOutIndx1).Cells("id").Value
                Dim cur_DT_goods As DataTable
                cur_DT_goods = Await Load_Detail("prx_SalOut", cur_id1, "&fetchPlan=salOut-full", "salOutLocalDetail")
                Me.Dgv_SalOutDet.DataSource = cur_DT_goods
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Dgv_Order_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles Dgv_Order.CellMouseDown
        cfli_OrderIndx1 = e.RowIndex
        If cflg_DetCamot1 Then
            cflg_DetCamot1 = False
            Load_OrderDet()
            cflg_DetCamot1 = True
        End If
    End Sub

    Private Sub Dgv_Order_SelectionChanged(sender As Object, e As EventArgs) Handles Dgv_Order.SelectionChanged
        If cflg_DetCamot1 Then
            cflg_DetCamot1 = False
            Try
                If Me.Dgv_Order.SelectedCells.Count > 0 Then
                    Me.Dgv_OrderDet.DataSource = ""
                    cfli_OrderIndx1 = Me.Dgv_Order.SelectedCells(0).RowIndex
                    Load_OrderDet()
                End If
            Catch ex As Exception
            End Try
            cflg_DetCamot1 = True
        End If
    End Sub

    Private Async Sub Load_OrderDet()
        Try
            If cfli_OrderIndx1 >= 0 Then
                Me.Dgv_OrderDet.DataSource = ""
                cfli_OrderIndx1 = Me.Dgv_Order.SelectedCells(0).RowIndex
                Dim cur_id1 As String = Me.Dgv_Order.Rows(cfli_OrderIndx1).Cells("id").Value
                Dim cur_DT_goods As DataTable
                cur_DT_goods = Await Load_Detail("prx_Order", cur_id1, "&fetchPlan=ourder-full", "orderLocalDetail")
                Me.Dgv_OrderDet.DataSource = cur_DT_goods
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Dgv_OutletDebt_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles Dgv_OutletDebt.CellMouseDown
        cfli_OutletDebtIndx1 = e.RowIndex
        If cflg_DetCamot1 Then
            cflg_DetCamot1 = False
            Load_OutletDebtDet()
            cflg_DetCamot1 = True
        End If
    End Sub

    Private Sub Dgv_OutletDebt_SelectionChanged(sender As Object, e As EventArgs) Handles Dgv_OutletDebt.SelectionChanged
        If cflg_DetCamot1 Then
            cflg_DetCamot1 = False
            Try
                If Me.Dgv_OutletDebt.SelectedCells.Count > 0 Then
                    Me.Dgv_OutletDebtDet.DataSource = ""
                    cfli_OutletDebtIndx1 = Me.Dgv_OutletDebt.SelectedCells(0).RowIndex
                    Load_OutletDebtDet()
                End If
            Catch ex As Exception
            End Try
            cflg_DetCamot1 = True
        End If
    End Sub

    Private Async Sub Load_OutletDebtDet()
        Try
            If cfli_OutletDebtIndx1 >= 0 Then
                Me.Dgv_OutletDebtDet.DataSource = ""
                cfli_OutletDebtIndx1 = Me.Dgv_OutletDebt.SelectedCells(0).RowIndex
                Dim cur_id1 As String = Me.Dgv_OutletDebt.Rows(cfli_OutletDebtIndx1).Cells("id").Value
                Dim DT_OutletDebtDet As DataTable = Await Load_DetailOutletDebt("prx_OutletDebt", cur_id1,
                                    "&fetchPlan=outletDebt-fetch-plan", "outletDebtsDetail")
                Me.Dgv_OutletDebtDet.DataSource = DT_OutletDebtDet
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Dgv_SalInDet_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles Dgv_SalInDet.CellMouseDown
        cfli_SalInDetIndx1 = e.RowIndex
    End Sub

    Private Sub Dgv_SalOutDet_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles Dgv_SalOutDet.CellMouseDown
        cfli_SalOutDetIndx1 = e.RowIndex
    End Sub

    Private Sub Dgv_OrderDet_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles Dgv_OrderDet.CellMouseDown
        cfli_OrderDetIndx1 = e.RowIndex
    End Sub

    Private Sub Dgv_OutletDebtDet_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles Dgv_OutletDebtDet.CellMouseDown
        cfli_OutletDebtDetIndx1 = e.RowIndex
    End Sub

    Private Async Sub Tsmi_DelSalInDet_Click(sender As Object, e As EventArgs) Handles Tsmi_DelSalInDet.Click
        If cfli_SalInDetIndx1 < 0 Then
            MsgBox("შეარჩიეთ ჩანაწერი!", MsgBoxStyle.Exclamation, "აუცილებელი პირობა!")
            Return
        End If
        Dim rt1 As Rt_Class = Await JS_DeleteCurentID("prx_SalInLocalDetail", AccessibleDefaultActionDescription = Nothing)
        If Not rt1.Rt_flg Then
            MsgBox(rt1,, "")
            Exit Sub
        Else
            Me.Dgv_SalInDet.Rows.Remove(Me.Dgv_SalInDet.Rows(cfli_SalInDetIndx1))
        End If
    End Sub

    Private Async Sub Tsmi_SalOutDet_Click(sender As Object, e As EventArgs) Handles Tsmi_SalOutDet.Click
        If cfli_SalOutDetIndx1 < 0 Then
            MsgBox("შეარჩიეთ ჩანაწერი!", MsgBoxStyle.Exclamation, "აუცილებელი პირობა!")
            Return
        End If
        Dim id_det1 As String = Me.Dgv_SalOutDet.Rows(cfli_SalOutDetIndx1).Cells("id").Value
        Dim rt1 As Rt_Class = Await JS_DeleteCurentID("prx_SalOutLocalDetail", Me.Dgv_SalOutDet.Rows(cfli_SalOutDetIndx1).Cells("id").Value)
        If Not rt1.Rt_flg Then
            MsgBox(rt1,, "")
            Exit Sub
        Else
            Me.Dgv_SalOutDet.Rows.Remove(Me.Dgv_SalOutDet.Rows(cfli_SalOutDetIndx1))
        End If
    End Sub

    Private Async Sub Tsmi_OrderDet_Click(sender As Object, e As EventArgs) Handles Tsmi_OrderDet.Click
        If cfli_OrderDetIndx1 < 0 Then
            MsgBox("შეარჩიეთ ჩანაწერი!", MsgBoxStyle.Exclamation, "აუცილებელი პირობა!")
            Return
        End If
        Dim id_det1 As String = Me.Dgv_OrderDet.Rows(cfli_OrderDetIndx1).Cells("id").Value
        Dim rt1 As Rt_Class = Await JS_DeleteCurentID("prx_OrderLocalDetail", Me.Dgv_OrderDet.Rows(cfli_OrderDetIndx1).Cells("id").Value)
        If Not rt1.Rt_flg Then
            MsgBox(rt1,, "")
            Exit Sub
        Else
            Me.Dgv_OrderDet.Rows.Remove(Me.Dgv_OrderDet.Rows(cfli_OrderDetIndx1))
        End If
    End Sub

    Private Async Sub Tsmi_OutletDebtDet_Click(sender As Object, e As EventArgs) Handles Tsmi_OutletDebtDet.Click
        If cfli_OutletDebtDetIndx1 < 0 Then
            MsgBox("შეარჩიეთ ჩანაწერი!", MsgBoxStyle.Exclamation, "აუცილებელი პირობა!")
            Return
        End If
        Dim id_det1 As String = Me.Dgv_OutletDebtDet.Rows(cfli_OutletDebtDetIndx1).Cells("id").Value
        Dim rt1 As Rt_Class = Await JS_DeleteCurentID("prx_OutletDebtsDetail", Me.Dgv_OutletDebtDet.Rows(cfli_OutletDebtDetIndx1).Cells("id").Value)
        If Not rt1.Rt_flg Then
            MsgBox(rt1,, "")
            Exit Sub
        Else
            Me.Dgv_OutletDebtDet.Rows.Remove(Me.Dgv_OutletDebtDet.Rows(cfli_OutletDebtDetIndx1))
        End If
    End Sub

    Private Async Sub Bt_ForUploadParentCompany_Click(sender As Object, e As EventArgs) Handles Bt_ForUploadParentCompany.Click
        UpdateProgressBar0(0)
        Await Task.Delay(50)
        Dim rt_data As Rt_Class = ReadJsonArray("ParentCompany", M_Cur_cust_id)
        If rt_data.Rt_flg Then
            Dim cDT_UpParentCompany As DataTable = rt_data.Rt_Dt
            Dim rt_txt As String = ""
            ' ატვირთვის ნაწილი
            Dim dt_chan_rd1 As Integer = cDT_UpParentCompany.Rows.Count
            Dim cr_iter1 As Integer = Me.Nud_ParentCompany.Value
            For icr7 As Integer = 0 To dt_chan_rd1 - 1 Step cr_iter1
                Dim Sl_Rows1 As DataRow() = cDT_UpParentCompany.AsEnumerable().Skip(icr7).Take(cr_iter1).ToArray()
                Dim cjs_ParentCompany As New List(Of Cl_ParentCompany)
                If Sl_Rows1.Count > 0 Then
                    For Each dr22 As DataRow In Sl_Rows1
                        cjs_ParentCompany.Add(New Cl_ParentCompany With {.id = M_Cur_cust_id.ToString & Trim(dr22("id")),
                                              .pcomp_code = M_Cur_cust_id.ToString & Trim(dr22("pcomp_code")),
                                              .pc_vat_num = Trim(dr22("pc_vat_num")),
                                              .pc_name = Mid(Trim(dr22("pc_name")), 1, 50),
                                              .dtlm = DateTime.Now.ToString("yyyy-MM-ddTHH:mm"),
                                              .cust_id = M_Cur_cust_id})
                    Next
                    Dim jsonString As String = JsonConvert.SerializeObject(cjs_ParentCompany)

                    Dim rt_UpData As Rt_Class = Await CreateData("prx_ParentCompany", jsonString)
                    Try
                        rt_txt = rt_txt.Replace("]", ",")
                        If rt_txt.Length > 0 Then
                            rt_UpData.Rt_txt = rt_UpData.Rt_txt.Replace("[", "")
                        End If
                        rt_txt &= rt_UpData.Rt_txt
                    Catch ex As Exception
                        MsgBox(ex.Message & vbCrLf & rt_UpData.Rt_txt)
                        Exit Sub
                    End Try
                End If
                UpdateProgressBar0((icr7 + 1) * 100 / dt_chan_rd1)
                Await Task.Delay(50)
            Next
            UpdateProgressBar0(100)
            Await Task.Delay(50)
            ' შედეგის შენახვა
            Dim Ret_Filename As String = Application.StartupPath & "\JsonFiles\JSon_Return_ParentCompany " & DateTime.Now.ToString("yyyyMMddHHmmssfff") & ".txt"
            If Not My.Computer.FileSystem.DirectoryExists(Path.GetDirectoryName(Ret_Filename)) Then
                Directory.CreateDirectory(Path.GetDirectoryName(Ret_Filename))
            End If
            System.IO.File.WriteAllText(Ret_Filename, rt_txt)
        Else
            MsgBox(rt_data.Rt_txt,, "")
        End If
    End Sub

    Private Async Sub Bt_ForUploadOutlet_Click(sender As Object, e As EventArgs) Handles Bt_ForUploadOutlet.Click
        UpdateProgressBar0(0)
        Await Task.Delay(50)
        Dim rt_data As Rt_Class = ReadJsonArray("Outlet", M_Cur_cust_id)
        If rt_data.Rt_flg Then
            Dim cDT_UpOutlets As DataTable = rt_data.Rt_Dt
            Dim rt_txt As String = ""
            ' ატვირთვის ნაწილი
            Dim dt_chan_rd1 As Integer = cDT_UpOutlets.Rows.Count
            Dim cr_iter1 As Integer = Me.Nud_outlet.Value
            For icr7 As Integer = 0 To dt_chan_rd1 - 1 Step cr_iter1
                Dim Sl_Rows1 As DataRow() = cDT_UpOutlets.AsEnumerable().Skip(icr7).Take(cr_iter1).ToArray()
                Dim cjs_Outlet As New List(Of Cl_Outlets)
                If Sl_Rows1.Count > 0 Then
                    For Each dr22 As DataRow In Sl_Rows1
                        Dim cr_Pcomp_Code As New Cl_Pcomp_Code
                        cr_Pcomp_Code.id = M_Cur_cust_id.ToString & Trim(dr22("pcomp_code"))
                        cjs_Outlet.Add(New Cl_Outlets With {
                                              .id = CLng(M_Cur_cust_id.ToString & dr22("id")),
                                              .ol_id = CLng(M_Cur_cust_id.ToString & dr22("ol_id")),
                                              .ol_code = M_Cur_cust_id.ToString & dr22("ol_code"),
                                              .subtype_id = dr22("subtype_id"),
                                              .area_id = 0,
                                              .name = dr22("name"),
                                              .trade_name = dr22("trade_name"),
                                              .address = dr22("address"),
                                              .deliv_addr = dr22("deliv_addr"),
                                              .telephone = dr22("telephone"),
                                              .ipn = dr22("ipn"),
                                              .merch_code = M_Cur_cust_id.ToString & dr22("merch_code"),
                                              .pcomp_code = cr_Pcomp_Code,
                                              .status = dr22("status"),
                                              .dtlm = DateTime.Now.ToString("yyyy-MM-ddTHH:mm"),
                                              .cust_id = M_Cur_cust_id.ToString})
                    Next
                    Dim jsonString As String = JsonConvert.SerializeObject(cjs_Outlet)

                    Dim rt_UpData As Rt_Class = Await CreateData("prx_Outlet", jsonString)
                    Try
                        rt_txt = rt_txt.Replace("]", ",")
                        If rt_txt.Length > 0 Then
                            rt_UpData.Rt_txt = rt_UpData.Rt_txt.Replace("[", "")
                        End If
                        rt_txt &= rt_UpData.Rt_txt
                    Catch ex As Exception
                        MsgBox(ex.Message & vbCrLf & rt_UpData.Rt_txt)
                        Exit Sub
                    End Try
                End If
                UpdateProgressBar0((icr7 + 1) * 100 / dt_chan_rd1)
                Await Task.Delay(50)
            Next
            UpdateProgressBar0(100)
            Await Task.Delay(50)
            ' შედეგის შენახვა
            Dim Ret_Filename As String = Application.StartupPath & "\JsonFiles\JSon_Return_Outlet " & DateTime.Now.ToString("yyyyMMddHHmmssfff") & ".txt"
            If Not My.Computer.FileSystem.DirectoryExists(Path.GetDirectoryName(Ret_Filename)) Then
                Directory.CreateDirectory(Path.GetDirectoryName(Ret_Filename))
            End If
            System.IO.File.WriteAllText(Ret_Filename, rt_txt)
        Else
            MsgBox(rt_data.Rt_txt,, "")
        End If
    End Sub

    Private Async Sub Bt_UpSalIns_Click(sender As Object, e As EventArgs) Handles Bt_UpSalIns.Click
        UpdateProgressBar0(0)
        Await Task.Delay(50)
        Dim rt_txt As String = ""
        Dim rt_data As Rt_Class = Read_JS_SalIn()
        If rt_data.Rt_flg Then
            Dim cDT_UpSalIns As DataTable = rt_data.Rt_Ds.Tables("Invoices")
            Dim icr7 As Integer = 0
            Dim dt_chan_rd1 As Integer = cDT_UpSalIns.Rows.Count
            ' ატვირთვის ნაწილი
            For Each dr_salIn As DataRow In cDT_UpSalIns.Rows
                icr7 += 1
                Dim cjs_Add_SalInsD As New List(Of Cl_SalInLocalDetail)
                Dim _invoice_no As Integer = dr_salIn("invoice_no")
                Dim SlDr_SalInsD As DataRow() = rt_data.Rt_Ds.Tables("SalInLocalDetails").Select($"invoice_no={_invoice_no}")
                For Each dr_SalInD As DataRow In SlDr_SalInsD
                    Dim cr_LocalCode As New Cl_SubID
                    cr_LocalCode.id = Trim(dr_SalInD("localcode"))
                    cjs_Add_SalInsD.Add(New Cl_SalInLocalDetail With {
                            .localcode = cr_LocalCode,
                            .lot_id = dr_SalInD("lot_id"),
                            .price = dr_SalInD("price"),
                            .qty = dr_SalInD("qty"),
                            .vat = dr_SalInD("vat"),
                            .status = dr_SalInD("status"),
                            .dtlm = CDate(dr_SalInD("dtlm")).ToString("yyyy-MM-ddTHH:mm"),
                            .cust_id = M_Cur_cust_id})
                Next

                Dim cjs_Add_SalInsH As New List(Of Cl_SalIn)
                cjs_Add_SalInsH.Add(New Cl_SalIn With {.invoice_no = M_Cur_cust_id & dr_salIn("invoice_no"),
                            .[date] = CDate(dr_salIn("date")).ToString("yyyy-MM-dd"),
                            .wareh_code = M_Cur_cust_id & dr_salIn("wareh_code"),
                            .doc_type = dr_salIn("doc_type"),
                            .custdoc_no = dr_salIn("custdoc_no"),
                            .vatcalcmod = dr_salIn("vatcalcmod"),
                            .status = dr_salIn("status"),
                            .salInLocalDetail = cjs_Add_SalInsD,
                            .dtlm = CDate(dr_salIn("dtlm")).ToString("yyyy-MM-ddTHH:mm"),
                            .cust_id = M_Cur_cust_id})
                Dim jsonString As String = JsonConvert.SerializeObject(cjs_Add_SalInsH)

                Dim rt_UpData As Rt_Class = Await CreateData("prx_SalIn", jsonString)
                Try
                    rt_txt = rt_txt.Replace("]", ",")
                    If rt_txt.Length > 0 Then
                        rt_UpData.Rt_txt = rt_UpData.Rt_txt.Replace("[", "")
                    End If
                    rt_txt &= rt_UpData.Rt_txt
                Catch ex As Exception
                    MsgBox(ex.Message & vbCrLf & rt_UpData.Rt_txt)
                    Exit Sub
                End Try
                UpdateProgressBar0(icr7 * 100 / dt_chan_rd1)
                Await Task.Delay(50)
            Next
        End If

        UpdateProgressBar0(100)
        Await Task.Delay(50)
        ' შედეგის შენახვა
        Dim Ret_Filename As String = Application.StartupPath & "\JsonFiles\JSon_Return_SalIn " & DateTime.Now.ToString("yyyyMMddHHmmssfff") & ".txt"
        If Not My.Computer.FileSystem.DirectoryExists(Path.GetDirectoryName(Ret_Filename)) Then
            Directory.CreateDirectory(Path.GetDirectoryName(Ret_Filename))
        End If
        System.IO.File.WriteAllText(Ret_Filename, rt_txt)
    End Sub
End Class
