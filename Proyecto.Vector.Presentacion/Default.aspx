<%@ Page Title="Gestión de Vectores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Proyecto.Vector.Presentacion._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="card shadow-lg p-4 rounded-3" style="max-width: 700px; margin: auto; background-color: #f9f9f9;">
            <h2 class="text-center mb-4 text-primary fw-bold">Gestión de Vectores</h2>

            <!-- CA1: Solicitar tamaño -->
            <div class="mb-3">
                <asp:Label ID="lblN" runat="server" Text="Ingrese tamaño del vector:" CssClass="form-label fw-semibold" />
                <asp:TextBox ID="txtN" runat="server" CssClass="form-control" />
            </div>
            <div class="d-grid mb-3">
                <asp:Button ID="btnGenerar" runat="server" Text="Generar Campos" CssClass="btn btn-primary" OnClick="btnGenerar_Click" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-info fw-bold" /><br />

            <!-- CA2: Panel con TextBox dinámicos -->
            <asp:Panel ID="panelCampos" runat="server" CssClass="mb-3"></asp:Panel>
            <div class="d-grid mb-3">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Vector" CssClass="btn btn-success" OnClick="btnGuardar_Click" Visible="false" />
            </div>

            <!-- Mostrar vector -->
            <h5 class="text-secondary fw-bold">Vector A:</h5>
            <div class="alert alert-secondary">
                <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
            </div>

            <!-- CA3: Operaciones -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnCalcular" runat="server" Text="Calcular Promedio y Desviación" CssClass="btn btn-outline-primary" OnClick="btnCalcular_Click" />
                <asp:Button ID="btnMaxMin" runat="server" Text="Máximo y Mínimo" CssClass="btn btn-outline-secondary" OnClick="btnMaxMin_Click" />
                <asp:Button ID="btnPalindromo" runat="server" Text="Verificar Palíndromo" CssClass="btn btn-outline-warning" OnClick="btnPalindromo_Click" />
            </div>

            <!-- NUEVA SECCIÓN: Búsqueda de Subvector -->
            <h5 class="text-secondary fw-bold">Búsqueda de Subvector:</h5>
            <div class="mb-3">
                <asp:Label ID="lblSubvectorSize" runat="server" Text="Tamaño del subvector B:" CssClass="form-label fw-semibold" />
                <asp:TextBox ID="txtSubvectorSize" runat="server" CssClass="form-control" />
            </div>
            <div class="d-grid mb-3">
                <asp:Button ID="btnGenerarSubvector" runat="server" Text="Generar Campos Subvector" CssClass="btn btn-info text-white" OnClick="btnGenerarSubvector_Click" />
            </div>
            <asp:Panel ID="panelSubvector" runat="server" CssClass="mb-3"></asp:Panel>
            <div class="d-grid mb-4">
                <asp:Button ID="btnBuscarSubvector" runat="server" Text="Buscar Subvector" CssClass="btn btn-dark" OnClick="btnBuscarSubvector_Click" />
            </div>

            <!-- NUEVA SECCIÓN: Producto Escalar -->
            <h5 class="text-secondary fw-bold">Producto Escalar:</h5>
            <div class="mb-3">
                <asp:Label ID="lblVector2Size" runat="server" Text="Tamaño del vector 2:" CssClass="form-label fw-semibold" />
                <asp:TextBox ID="txtVector2Size" runat="server" CssClass="form-control" />
            </div>
            <div class="d-grid mb-3">
                <asp:Button ID="btnGenerarVector2" runat="server" Text="Generar Campos Vector 2" CssClass="btn btn-info text-white" OnClick="btnGenerarVector2_Click" />
            </div>
            <asp:Panel ID="panelVector2" runat="server" CssClass="mb-3"></asp:Panel>
            <div class="d-grid mb-4">
                <asp:Button ID="btnProductoEscalar" runat="server" Text="Calcular Producto Escalar" CssClass="btn btn-dark" OnClick="btnProductoEscalar_Click" />
            </div>

            <!-- Resultados -->
            <h5 class="text-secondary fw-bold">Resultados:</h5>
            <div class="alert alert-info">
                <asp:Label ID="lblEstadisticas" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>