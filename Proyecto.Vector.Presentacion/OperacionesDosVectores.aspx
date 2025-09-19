<%@ Page Title="Operaciones con 2 Vectores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OperacionesDosVectores.aspx.cs" Inherits="Proyecto.Vector.Presentacion.DosVectores" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="card shadow-lg p-4 rounded-3" style="max-width: 700px; margin: auto; background-color: #f9f9f9;">
            <h2 class="text-center mb-4 text-primary fw-bold">Operaciones con Dos Vectores</h2>

            <!-- SECCIÓN VECTOR A -->
            <h5 class="text-secondary fw-bold">Vector Principal (A):</h5>
            <div class="alert alert-secondary">
                <asp:Label ID="lblVectorA" runat="server" Text="Vector no cargado. Por favor, cargue uno en la página principal." />
            </div>
            
            <asp:Label ID="lblMensaje" runat="server" CssClass="text-info fw-bold" /><br />
            
            <!-- SECCIÓN VECTOR B -->
            <h5 class="text-secondary fw-bold mt-4">Crear Vector B:</h5>
            <div class="mb-3">
                <asp:Label ID="lblVectorBSize" runat="server" Text="Tamaño del Vector B:" CssClass="form-label fw-semibold" />
                <asp:TextBox ID="txtVectorBSize" runat="server" CssClass="form-control" />
            </div>
            <div class="d-grid mb-3">
                <asp:Button ID="btnGenerarVectorB" runat="server" Text="Generar Campos" CssClass="btn btn-primary" OnClick="btnGenerarVectorB_Click" />
            </div>

            <!-- Panel con TextBox dinámicos -->
            <asp:Panel ID="panelVectorB" runat="server" CssClass="mb-3"></asp:Panel>
            
            <div class="d-grid mb-3">
                <asp:Button ID="btnGuardarVectorB" runat="server" Text="Guardar Vector" CssClass="btn btn-success" OnClick="btnGuardarVectorB_Click" Visible="false" />
            </div>

            <!-- Mostrar vector B guardado -->
            <h5 class="text-secondary fw-bold">Vector B:</h5>
            <div class="alert alert-secondary">
                <asp:Label ID="lblVectorBGuardado" runat="server" Text="No hay vector B guardado"></asp:Label>
            </div>

            <!-- OPERACIONES - Mismo estilo que Default.aspx -->
            <h5 class="text-secondary fw-bold">Operaciones:</h5>
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnBuscarSubvector" runat="server" Text="Buscar Subvector B en A" CssClass="btn btn-outline-warning" OnClick="btnBuscarSubvector_Click" />
                <asp:Button ID="btnProductoEscalar" runat="server" Text="Calcular Producto Escalar A·B" CssClass="btn btn-outline-info" OnClick="btnProductoEscalar_Click" />
                <asp:Button ID="btnIntercalar" runat="server" Text="Intercalar Vectores A y B" CssClass="btn btn-outline-secondary" OnClick="btnIntercalar_Click" />
                <asp:Button ID="btnFusionOrdenada" runat="server" Text="Fusionar y Ordenar A y B" CssClass="btn btn-outline-dark" OnClick="btnFusionOrdenada_Click" />
            </div>
            
            <!-- RESULTADOS - Mismo estilo que Default.aspx -->
            <h5 class="text-secondary fw-bold">Resultados:</h5>
            <div class="alert alert-info">
                <asp:Label ID="lblResultados" runat="server" Text="Esperando operación..."></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>