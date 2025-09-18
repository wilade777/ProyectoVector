<%@ Page Title="Gestión de Vectores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Proyecto.Vector.Presentacion._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="max-width: 600px; margin: 30px auto; padding: 20px; border: 1px solid #ccc; border-radius: 8px; font-family: Arial, sans-serif;">
        <h2>Gestión de Vectores</h2>

        <!-- CA1: Solicitar tamaño -->
        <asp:Label ID="lblN" runat="server" Text="Ingrese tamaño del vector:" />
        <asp:TextBox ID="txtN" runat="server" />
        <asp:Button ID="btnGenerar" runat="server" Text="Generar Campos" OnClick="btnGenerar_Click" />
        <br /><br />
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" /><br />

        <!-- CA2: Panel con TextBox dinámicos -->
        <asp:Panel ID="panelCampos" runat="server"></asp:Panel>
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Vector" OnClick="btnGuardar_Click" Visible="false" />
        <br /><br />

        <!-- Mostrar vector -->
        <h3>Vector:</h3>
        <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
        <br /><br />

        <!-- CA3: Operaciones -->
        <asp:Button ID="btnCalcular" runat="server" Text="Calcular Promedio y Desviación" OnClick="btnCalcular_Click" Visible="true" />
        
          <br /><br />

         <!-- maximo y minimo -->
        <asp:Button ID="btnMaxMin" runat="server" Text="Máximo y Mínimo" OnClick="btnMaxMin_Click"  />
        <asp:Label ID="lblMaxMin" runat="server" Text=""></asp:Label>


        <br /><br />

        <!-- Resultados -->
        <h3>Resultados:</h3>
        <asp:Label ID="lblEstadisticas" runat="server" Text=""></asp:Label>

    </div>
</asp:Content>
