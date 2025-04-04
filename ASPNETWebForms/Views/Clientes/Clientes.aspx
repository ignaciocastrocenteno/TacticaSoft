<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Clientes.aspx.vb" Inherits="ASPNETWebForms.Clientes" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Listado de Clientes</title>
    <style type="text/css">
        .table {}
        #form1 {
            height: 1000px;
        }
    </style>
</head>
<body style="height: 1000px">
    <form id="form1" runat="server">
        <h2>Listado de Clientes</h2>
        <!-- ASP.NET artifact 'GridView' to display the clients list -->
        <!-- Integrated buttons to edit and delete clients with 'OnRowEditing' & 'OnRowDeleting', respectively -->
        <asp:Repeater ID="RepeaterClientes" runat="server">
            <HeaderTemplate>
                <table border="1">
                    <tr>
                        <th>ID</th>
                        <th>Nombre</th>
                        <th>Correo</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("ID") %></td>
                    <td><%# Eval("Nombre") %></td>
                    <td><%# Eval("Correo") %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>


        <h3>Agregar un nuevo cliente:</h3>
        <!-- Form fields to add a new client information -->
        <ul>
            <li>
                Nombre:
                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
            </li>
            <li>
                Teléfono:
                <asp:TextBox ID="txtTelefono" runat="server" Width="128px"></asp:TextBox>
            </li>
            <li>
                Correo Electrónico:
                <asp:TextBox ID="txtCorreo" runat="server" Width="129px"></asp:TextBox>
            </li>
        </ul>
        <!-- Button to release the creation of a client object -->
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar Cliente" OnClick="btnAgregar_Click" />
        <asp:Label ID="agregarClienteResultado" runat="server" ForeColor="Red"></asp:Label>
        <hr />

        <h3>Buscar un cliente por ID:</h3>
        <ul>
            <li>
                Ingrese el ID del cliente buscado:
                <asp:TextBox ID="txtBuscarId" runat="server" />
            </li>
        </ul>
         <asp:Button ID="btnBuscar" runat="server" Text="Buscar Cliente" OnClick="btnBuscar_Click" />
         <asp:Label ID="buscarClienteResultado" runat="server" ForeColor="Red"></asp:Label>
         <hr />

        <h3>Actualizar la información de un cliente:</h3>
        <ul>
            <li>
                <label for="txtActualizarId">ID del Cliente:</label>
                <asp:TextBox ID="txtActualizarId" runat="server"></asp:TextBox>
            </li>
<%--            <li>
                Nombre:
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </li>--%>
            <<li>
                 <label for="txtActualizarNombre">Nombre:</label>
                 <asp:TextBox ID="txtActualizarNombre" runat="server"></asp:TextBox>
             </li>
<%--            <li>
                Teléfono:
                <asp:TextBox ID="TextBox2" runat="server" Width="128px"></asp:TextBox>
            </li>--%>
            <li>
                <label for="txtActualizarTelefono">Teléfono:</label>
                <asp:TextBox ID="txtActualizarTelefono" runat="server"></asp:TextBox>
            </li>
<%--            <li>
                Correo Electrónico:
                <asp:TextBox ID="TextBox3" runat="server" Width="129px"></asp:TextBox>
            </li>--%>
            <li>
                <label for="txtActualizarCorreo">Correo Electrónico:</label>
                <asp:TextBox ID="txtActualizarCorreo" runat="server"></asp:TextBox>
            </li>
        </ul>
         <asp:Button ID="btnActualizar" runat="server" Text="Actualizar información del cliente" OnClick="btnActualizar_Click" />
         &nbsp;&nbsp;&nbsp;
         <asp:Label ID="actualizarInformacionResultado" runat="server" ForeColor="Red"></asp:Label>
        <hr />

        <h3>Eliminar un cliente:</h3>
        <ul>
            <li>
                Nombre:
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            </li>
            <li>
                Teléfono:
                <asp:TextBox ID="TextBox5" runat="server" Width="128px"></asp:TextBox>
            </li>
            <li>
                Correo Electrónico:
                <asp:TextBox ID="TextBox6" runat="server" Width="129px"></asp:TextBox>
            </li>
        </ul>
         <asp:Button ID="btnEliminar" runat="server" Text="Actualizar información del cliente" OnClick="btnEliminar_Click" />
         &nbsp;&nbsp;&nbsp;
         <asp:Label ID="eliminarClienteResultado" runat="server" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
