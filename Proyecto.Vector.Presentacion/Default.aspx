<%@ Page Title="Operaciones con Vectores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Proyecto.Vector.Presentacion._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Operaciones con Vectores</h2>
    <div>
     <h2>Leer y Mostrar Vector</h2>

    <asp:Panel ID="pnlDefinirTamano" runat="server">
        <label>Introduce la dimensión del vector (N):</label><br />
        <asp:TextBox ID="txtTamano" runat="server" TextMode="Number"></asp:TextBox><br /><br />
        <asp:Button ID="btnDefinirTamano" runat="server" Text="Definir Tamaño" OnClick="btnDefinirTamano_Click" />
    </asp:Panel>

    <asp:Panel ID="pnlIngresarElementos" runat="server" Visible="false">
        <label>Introduce los elementos del vector (separar por comas):</label><br />
        <asp:TextBox ID="txtElementos" runat="server"></asp:TextBox><br /><br />
        <asp:Button ID="btnMostrarVector" runat="server" Text="Mostrar Vector" OnClick="btnMostrarVector_Click" />
    </asp:Panel>

    <h3>Vector:</h3>
    <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>