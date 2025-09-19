<%@ Page Title="Análisis Avanzado de Vectores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AnalisisAvanzado.aspx.cs" Inherits="Proyecto.Vector.Presentacion.AnalisisAvanzado" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="card shadow-lg p-4 rounded-3" style="max-width: 700px; margin: auto; background-color: #f9f9f9;">
            <h2 class="text-center mb-4 text-primary fw-bold">Análisis Avanzado de Vectores</h2>

            <!-- Solicitar tamaño -->
            <div class="mb-3">
                <asp:Label ID="lblN" runat="server" Text="Ingrese tamaño del vector:" CssClass="form-label fw-semibold" />
                <asp:TextBox ID="txtN" runat="server" CssClass="form-control" />
            </div>
            <div class="d-grid mb-3">
                <asp:Button ID="btnGenerar" runat="server" Text="Generar Campos" CssClass="btn btn-primary" OnClick="btnGenerar_Click" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-info fw-bold" /><br />

            <!-- Panel con TextBox dinámicos -->
            <asp:Panel ID="panelCampos" runat="server" CssClass="mb-3"></asp:Panel>
            <div class="d-grid mb-3">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Vector" CssClass="btn btn-success" OnClick="btnGuardar_Click" Visible="false" />
            </div>

            <!-- Mostrar vector -->
            <h5 class="text-secondary fw-bold">Vector A:</h5>
            <div class="alert alert-secondary">
                <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
            </div>

            <!-- Nuevas operaciones avanzadas -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnSubvectorCreciente" runat="server" Text="Subvector Creciente Más Largo" CssClass="btn btn-outline-primary" OnClick="btnSubvectorCreciente_Click" />
                <asp:Button ID="btnSegundoMayor" runat="server" Text="Segundo Valor Mayor" CssClass="btn btn-outline-secondary" OnClick="btnSegundoMayor_Click" />
                <asp:Button ID="btnConteoSecuencias" runat="server" Text="Conteo de Secuencias Iguales" CssClass="btn btn-outline-warning" OnClick="btnConteoSecuencias_Click" />
                <asp:Button ID="btnReemplazoFrecuencias" runat="server" Text="Reemplazo por Frecuencias" CssClass="btn btn-outline-info" OnClick="btnReemplazoFrecuencias_Click" />
                <asp:Button ID="btnBalancePosNeg" runat="server" Text="Balance Positivo/Negativo" CssClass="btn btn-outline-dark" OnClick="btnBalancePosNeg_Click" />
                <asp:Button ID="btnMayorDiferencia" runat="server" Text="Mayor Diferencia" CssClass="btn btn-outline-danger" OnClick="btnMayorDiferencia_Click" />
            </div>
                           
             <!-- Resultados -->
            <h5 class="text-secondary fw-bold">Resultados:</h5>
            <div class="alert alert-info">
                <asp:Label ID="lblEstadisticas" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>

