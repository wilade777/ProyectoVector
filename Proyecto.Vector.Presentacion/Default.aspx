<%@ Page Title="Operaciones con Vectores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Proyecto.Vector.Presentacion._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Operaciones con Vectores</h2>

    <!-- Paso 1: ingresar tamaño del vector -->
    <asp:Label ID="lblN" runat="server" Text="Ingrese tamaño del vector:" />
    <asp:TextBox ID="txtN" runat="server" />
    <asp:Button ID="btnGenerar" runat="server" Text="Generar Campos" OnClick="btnGenerar_Click" />
    <br /><br />

    <!-- Mensajes de error o información -->
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />
    <br />

    <!-- Paso 2: Panel para los campos dinámicos -->
    <asp:Panel ID="panelCampos" runat="server"></asp:Panel>
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Vector" OnClick="btnGuardar_Click" Visible="false" />
    <br /><br />

    <!-- Paso 3: mostrar vector -->
    <h3>Vector:</h3>
    <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
</asp:Content>
