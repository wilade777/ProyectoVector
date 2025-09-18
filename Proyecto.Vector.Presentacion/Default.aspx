<%@ Page Title="Operaciones con Vectores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Proyecto.Vector.Presentacion._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Operaciones con Vectores</h2>
    <div>
      <label>Introduce los elementos del vector (separar por comas):</label><br />
        <asp:TextBox ID="txtVector" runat="server"></asp:TextBox><br /><br />
        
        <asp:Button ID="btnMostrar" runat="server" Text="Mostrar Vector" OnClick="btnMostrar_Click" /><br /><br />
        
        <h3>Vector:</h3>
        <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>