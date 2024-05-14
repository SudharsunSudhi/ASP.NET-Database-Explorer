<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Task_1._Default" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Database Explorer</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Database Explorer</h2>
            
            <!-- Step 1: Connection String Input -->
            <asp:Label runat="server" AssociatedControlID="txtConnectionString">Enter Connection String:</asp:Label>
            <asp:TextBox ID="txtConnectionString" runat="server" CssClass="btn btn-success"></asp:TextBox>    
            <br /> <br /> <br />
            <asp:Button ID="btnTestConnection" runat="server" Text="Test Connection" OnClick="btnTestConnection_Click" />
            <br />
            <asp:Label ID="lblConnectionStatus" runat="server"></asp:Label>
            
            <!-- Step 2: Display Tables -->
            <div id="tablesContainer" runat="server" style="display:none;">
                <h3>Tables</h3>
                <asp:DropDownList ID="ddlTables" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTables_SelectedIndexChanged">
                </asp:DropDownList>
                
                <!-- Step 3: Display Columns -->
                <div id="columnsContainer" runat="server" style="display:none;">
                    <h3>Columns</h3>
                    <asp:BulletedList ID="bulletedColumns" runat="server">
                    </asp:BulletedList>
                </div>

                <!-- Step 4: Query Execution -->
            <div>
            <h2>Query Executor</h2>
            
            <!-- Textbox for entering the query -->
            <asp:TextBox ID="txtQuery" runat="server" TextMode="MultiLine" Rows="4" Columns="50"></asp:TextBox>
            <br />
            <!-- Button to execute the query -->
            <asp:Button ID="btnExecuteQuery" runat="server" Text="Execute Query" OnClick="btnExecuteQuery_Click" />
            <br />
            <!-- GridView to display query results -->
            <asp:GridView ID="gridQueryResults" runat="server" AutoGenerateColumns="true">
            </asp:GridView>
        </div>
            
            
        </div>
    </form>
</body>
</html>
