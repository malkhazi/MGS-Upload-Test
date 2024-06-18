<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Admin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Bt_Save = New System.Windows.Forms.Button()
        Me.Mtb_time = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Rb_http1 = New System.Windows.Forms.RadioButton()
        Me.Rb_https1 = New System.Windows.Forms.RadioButton()
        Me.Tb_ClientId1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Tb_ClientSecret1 = New System.Windows.Forms.TextBox()
        Me.Tb_IP1 = New System.Windows.Forms.TextBox()
        Me.gb_MSSQL = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Tb_TmpBaza = New System.Windows.Forms.TextBox()
        Me.Bt_mssql_shem = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Tb_serv_ps = New System.Windows.Forms.TextBox()
        Me.Tb_baza = New System.Windows.Forms.TextBox()
        Me.Tb_serv_user = New System.Windows.Forms.TextBox()
        Me.Tb_serv_port = New System.Windows.Forms.TextBox()
        Me.Tb_serv_ip = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Tb_key_klGm = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Tb_ClientNik1 = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Tb_cust_id1 = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Tb_ClientNik2 = New System.Windows.Forms.TextBox()
        Me.Tb_ClientSecret2 = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Tb_cust_id2 = New System.Windows.Forms.TextBox()
        Me.Tb_ClientId2 = New System.Windows.Forms.TextBox()
        Me.Tb_IP2 = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Rb_addr2 = New System.Windows.Forms.RadioButton()
        Me.Rb_addr1 = New System.Windows.Forms.RadioButton()
        Me.gb_MSSQL.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Bt_Save
        '
        Me.Bt_Save.Location = New System.Drawing.Point(445, 359)
        Me.Bt_Save.Name = "Bt_Save"
        Me.Bt_Save.Size = New System.Drawing.Size(81, 33)
        Me.Bt_Save.TabIndex = 2
        Me.Bt_Save.Text = "შენახვა"
        Me.Bt_Save.UseVisualStyleBackColor = True
        '
        'Mtb_time
        '
        Me.Mtb_time.Location = New System.Drawing.Point(191, 11)
        Me.Mtb_time.Mask = "00:00"
        Me.Mtb_time.Name = "Mtb_time"
        Me.Mtb_time.Size = New System.Drawing.Size(67, 25)
        Me.Mtb_time.TabIndex = 3
        Me.Mtb_time.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(70, 14)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 18)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "ატვირთვის დრო:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 82)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 18)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "მისამართი 1:"
        '
        'Rb_http1
        '
        Me.Rb_http1.AutoSize = True
        Me.Rb_http1.Checked = True
        Me.Rb_http1.Location = New System.Drawing.Point(113, 51)
        Me.Rb_http1.Name = "Rb_http1"
        Me.Rb_http1.Size = New System.Drawing.Size(59, 22)
        Me.Rb_http1.TabIndex = 7
        Me.Rb_http1.TabStop = True
        Me.Rb_http1.Text = "http://"
        Me.Rb_http1.UseVisualStyleBackColor = True
        '
        'Rb_https1
        '
        Me.Rb_https1.AutoSize = True
        Me.Rb_https1.Location = New System.Drawing.Point(195, 51)
        Me.Rb_https1.Name = "Rb_https1"
        Me.Rb_https1.Size = New System.Drawing.Size(63, 22)
        Me.Rb_https1.TabIndex = 8
        Me.Rb_https1.Text = "https://"
        Me.Rb_https1.UseVisualStyleBackColor = True
        '
        'Tb_ClientId1
        '
        Me.Tb_ClientId1.Location = New System.Drawing.Point(135, 49)
        Me.Tb_ClientId1.Name = "Tb_ClientId1"
        Me.Tb_ClientId1.Size = New System.Drawing.Size(162, 25)
        Me.Tb_ClientId1.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 52)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 18)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "მომხმარებელი:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(68, 83)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 18)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "პაროლი:"
        '
        'Tb_ClientSecret1
        '
        Me.Tb_ClientSecret1.Location = New System.Drawing.Point(135, 80)
        Me.Tb_ClientSecret1.Name = "Tb_ClientSecret1"
        Me.Tb_ClientSecret1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.Tb_ClientSecret1.Size = New System.Drawing.Size(162, 25)
        Me.Tb_ClientSecret1.TabIndex = 9
        '
        'Tb_IP1
        '
        Me.Tb_IP1.Location = New System.Drawing.Point(103, 79)
        Me.Tb_IP1.Name = "Tb_IP1"
        Me.Tb_IP1.Size = New System.Drawing.Size(195, 25)
        Me.Tb_IP1.TabIndex = 11
        '
        'gb_MSSQL
        '
        Me.gb_MSSQL.Controls.Add(Me.Label6)
        Me.gb_MSSQL.Controls.Add(Me.Tb_TmpBaza)
        Me.gb_MSSQL.Controls.Add(Me.Bt_mssql_shem)
        Me.gb_MSSQL.Controls.Add(Me.Label7)
        Me.gb_MSSQL.Controls.Add(Me.Label11)
        Me.gb_MSSQL.Controls.Add(Me.Label8)
        Me.gb_MSSQL.Controls.Add(Me.Label9)
        Me.gb_MSSQL.Controls.Add(Me.Label10)
        Me.gb_MSSQL.Controls.Add(Me.Tb_serv_ps)
        Me.gb_MSSQL.Controls.Add(Me.Tb_baza)
        Me.gb_MSSQL.Controls.Add(Me.Tb_serv_user)
        Me.gb_MSSQL.Controls.Add(Me.Tb_serv_port)
        Me.gb_MSSQL.Controls.Add(Me.Tb_serv_ip)
        Me.gb_MSSQL.Location = New System.Drawing.Point(316, 10)
        Me.gb_MSSQL.Name = "gb_MSSQL"
        Me.gb_MSSQL.Size = New System.Drawing.Size(393, 246)
        Me.gb_MSSQL.TabIndex = 22
        Me.gb_MSSQL.TabStop = False
        Me.gb_MSSQL.Text = "MS SQL Server"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 115)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(110, 18)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "დროებითი ბაზა:"
        '
        'Tb_TmpBaza
        '
        Me.Tb_TmpBaza.Location = New System.Drawing.Point(129, 112)
        Me.Tb_TmpBaza.Name = "Tb_TmpBaza"
        Me.Tb_TmpBaza.Size = New System.Drawing.Size(255, 25)
        Me.Tb_TmpBaza.TabIndex = 7
        '
        'Bt_mssql_shem
        '
        Me.Bt_mssql_shem.Location = New System.Drawing.Point(166, 208)
        Me.Bt_mssql_shem.Name = "Bt_mssql_shem"
        Me.Bt_mssql_shem.Size = New System.Drawing.Size(99, 29)
        Me.Bt_mssql_shem.TabIndex = 5
        Me.Bt_mssql_shem.Text = "შემოწმება"
        Me.Bt_mssql_shem.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(63, 176)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 18)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "პაროლი:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(84, 84)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(39, 18)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "ბაზა:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(63, 145)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(60, 18)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "ლოგინი:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(71, 53)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(52, 18)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "პორტი:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(45, 21)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(78, 18)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "მისამართი:"
        '
        'Tb_serv_ps
        '
        Me.Tb_serv_ps.Location = New System.Drawing.Point(129, 173)
        Me.Tb_serv_ps.Name = "Tb_serv_ps"
        Me.Tb_serv_ps.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.Tb_serv_ps.Size = New System.Drawing.Size(255, 25)
        Me.Tb_serv_ps.TabIndex = 4
        '
        'Tb_baza
        '
        Me.Tb_baza.Location = New System.Drawing.Point(129, 81)
        Me.Tb_baza.Name = "Tb_baza"
        Me.Tb_baza.Size = New System.Drawing.Size(255, 25)
        Me.Tb_baza.TabIndex = 2
        '
        'Tb_serv_user
        '
        Me.Tb_serv_user.Location = New System.Drawing.Point(129, 142)
        Me.Tb_serv_user.Name = "Tb_serv_user"
        Me.Tb_serv_user.Size = New System.Drawing.Size(255, 25)
        Me.Tb_serv_user.TabIndex = 3
        '
        'Tb_serv_port
        '
        Me.Tb_serv_port.Location = New System.Drawing.Point(129, 50)
        Me.Tb_serv_port.Name = "Tb_serv_port"
        Me.Tb_serv_port.Size = New System.Drawing.Size(63, 25)
        Me.Tb_serv_port.TabIndex = 1
        '
        'Tb_serv_ip
        '
        Me.Tb_serv_ip.Location = New System.Drawing.Point(129, 18)
        Me.Tb_serv_ip.Name = "Tb_serv_ip"
        Me.Tb_serv_ip.Size = New System.Drawing.Size(255, 25)
        Me.Tb_serv_ip.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(71, 198)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(173, 18)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "მომწოდებლის საიდ. კოდი:"
        '
        'Tb_key_klGm
        '
        Me.Tb_key_klGm.Location = New System.Drawing.Point(74, 219)
        Me.Tb_key_klGm.Name = "Tb_key_klGm"
        Me.Tb_key_klGm.Size = New System.Drawing.Size(143, 25)
        Me.Tb_key_klGm.TabIndex = 24
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Tb_ClientNik1)
        Me.GroupBox1.Controls.Add(Me.Tb_ClientSecret1)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Tb_cust_id1)
        Me.GroupBox1.Controls.Add(Me.Tb_ClientId1)
        Me.GroupBox1.Location = New System.Drawing.Point(713, 23)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(319, 148)
        Me.GroupBox1.TabIndex = 25
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "მომხმარებელი 1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(62, 21)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 18)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "შენიშვნა:"
        '
        'Tb_ClientNik1
        '
        Me.Tb_ClientNik1.Location = New System.Drawing.Point(135, 18)
        Me.Tb_ClientNik1.Name = "Tb_ClientNik1"
        Me.Tb_ClientNik1.Size = New System.Drawing.Size(162, 25)
        Me.Tb_ClientNik1.TabIndex = 11
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(79, 114)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(49, 18)
        Me.Label17.TabIndex = 1
        Me.Label17.Text = "cust_id:"
        '
        'Tb_cust_id1
        '
        Me.Tb_cust_id1.Location = New System.Drawing.Point(135, 111)
        Me.Tb_cust_id1.Name = "Tb_cust_id1"
        Me.Tb_cust_id1.Size = New System.Drawing.Size(162, 25)
        Me.Tb_cust_id1.TabIndex = 9
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Tb_ClientNik2)
        Me.GroupBox2.Controls.Add(Me.Tb_ClientSecret2)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.Tb_cust_id2)
        Me.GroupBox2.Controls.Add(Me.Tb_ClientId2)
        Me.GroupBox2.Location = New System.Drawing.Point(715, 177)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(319, 148)
        Me.GroupBox2.TabIndex = 26
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "მომხმარებელი 2"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(62, 21)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(66, 18)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "შენიშვნა:"
        '
        'Tb_ClientNik2
        '
        Me.Tb_ClientNik2.Location = New System.Drawing.Point(135, 18)
        Me.Tb_ClientNik2.Name = "Tb_ClientNik2"
        Me.Tb_ClientNik2.Size = New System.Drawing.Size(162, 25)
        Me.Tb_ClientNik2.TabIndex = 11
        '
        'Tb_ClientSecret2
        '
        Me.Tb_ClientSecret2.Location = New System.Drawing.Point(135, 80)
        Me.Tb_ClientSecret2.Name = "Tb_ClientSecret2"
        Me.Tb_ClientSecret2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.Tb_ClientSecret2.Size = New System.Drawing.Size(162, 25)
        Me.Tb_ClientSecret2.TabIndex = 9
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(79, 114)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(49, 18)
        Me.Label18.TabIndex = 1
        Me.Label18.Text = "cust_id:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(25, 52)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(103, 18)
        Me.Label14.TabIndex = 1
        Me.Label14.Text = "მომხმარებელი:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(68, 83)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(60, 18)
        Me.Label15.TabIndex = 1
        Me.Label15.Text = "პაროლი:"
        '
        'Tb_cust_id2
        '
        Me.Tb_cust_id2.Location = New System.Drawing.Point(135, 111)
        Me.Tb_cust_id2.Name = "Tb_cust_id2"
        Me.Tb_cust_id2.Size = New System.Drawing.Size(162, 25)
        Me.Tb_cust_id2.TabIndex = 9
        '
        'Tb_ClientId2
        '
        Me.Tb_ClientId2.Location = New System.Drawing.Point(135, 49)
        Me.Tb_ClientId2.Name = "Tb_ClientId2"
        Me.Tb_ClientId2.Size = New System.Drawing.Size(162, 25)
        Me.Tb_ClientId2.TabIndex = 9
        '
        'Tb_IP2
        '
        Me.Tb_IP2.Location = New System.Drawing.Point(103, 110)
        Me.Tb_IP2.Name = "Tb_IP2"
        Me.Tb_IP2.Size = New System.Drawing.Size(195, 25)
        Me.Tb_IP2.TabIndex = 28
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(8, 113)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(88, 18)
        Me.Label16.TabIndex = 27
        Me.Label16.Text = "მისამართი 2:"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Rb_addr2)
        Me.Panel1.Controls.Add(Me.Rb_addr1)
        Me.Panel1.Location = New System.Drawing.Point(15, 147)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(283, 39)
        Me.Panel1.TabIndex = 29
        '
        'Rb_addr2
        '
        Me.Rb_addr2.AutoSize = True
        Me.Rb_addr2.Location = New System.Drawing.Point(153, 8)
        Me.Rb_addr2.Name = "Rb_addr2"
        Me.Rb_addr2.Size = New System.Drawing.Size(103, 22)
        Me.Rb_addr2.TabIndex = 10
        Me.Rb_addr2.Text = "მისამართი 2"
        Me.Rb_addr2.UseVisualStyleBackColor = True
        '
        'Rb_addr1
        '
        Me.Rb_addr1.AutoSize = True
        Me.Rb_addr1.Checked = True
        Me.Rb_addr1.Location = New System.Drawing.Point(18, 8)
        Me.Rb_addr1.Name = "Rb_addr1"
        Me.Rb_addr1.Size = New System.Drawing.Size(103, 22)
        Me.Rb_addr1.TabIndex = 9
        Me.Rb_addr1.TabStop = True
        Me.Rb_addr1.Text = "მისამართი 1"
        Me.Rb_addr1.UseVisualStyleBackColor = True
        '
        'Frm_Admin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1044, 396)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Tb_IP2)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Tb_key_klGm)
        Me.Controls.Add(Me.gb_MSSQL)
        Me.Controls.Add(Me.Tb_IP1)
        Me.Controls.Add(Me.Rb_https1)
        Me.Controls.Add(Me.Rb_http1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Mtb_time)
        Me.Controls.Add(Me.Bt_Save)
        Me.Font = New System.Drawing.Font("Sylfaen", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Frm_Admin"
        Me.Text = "ადმინისტრირება"
        Me.gb_MSSQL.ResumeLayout(False)
        Me.gb_MSSQL.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Bt_Save As Button
    Friend WithEvents Mtb_time As MaskedTextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Rb_http1 As RadioButton
    Friend WithEvents Rb_https1 As RadioButton
    Friend WithEvents Tb_ClientId1 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Tb_ClientSecret1 As TextBox
    Friend WithEvents Tb_IP1 As TextBox
    Friend WithEvents gb_MSSQL As GroupBox
    Friend WithEvents Bt_mssql_shem As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Tb_serv_ps As TextBox
    Friend WithEvents Tb_baza As TextBox
    Friend WithEvents Tb_serv_user As TextBox
    Friend WithEvents Tb_serv_port As TextBox
    Friend WithEvents Tb_serv_ip As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Tb_TmpBaza As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Tb_key_klGm As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Tb_ClientNik1 As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Tb_ClientNik2 As TextBox
    Friend WithEvents Tb_ClientSecret2 As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Tb_ClientId2 As TextBox
    Friend WithEvents Tb_IP2 As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Rb_addr2 As RadioButton
    Friend WithEvents Rb_addr1 As RadioButton
    Friend WithEvents Label17 As Label
    Friend WithEvents Tb_cust_id1 As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Tb_cust_id2 As TextBox
End Class
