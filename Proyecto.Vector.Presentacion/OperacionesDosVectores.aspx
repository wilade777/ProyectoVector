<%@ Page Title="Operaciones con 2 Vectores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OperacionesDosVectores.aspx.cs" Inherits="Proyecto.Vector.Presentacion.DosVectores" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="card shadow-lg p-4 rounded-3" style="max-width: 800px; margin: auto; background-color: #f9f9f9;">
            <h2 class="text-center mb-4 text-primary fw-bold">Operaciones con Dos Vectores</h2>

            <h5 class="text-secondary fw-bold">Vector Principal (A):</h5>
            <div class="alert alert-secondary">
                <asp:Label ID="lblVectorA" runat="server" Text="Vector no cargado. Por favor, cargue uno en la página principal." />
            </div>
            
            <asp:Label ID="lblMensaje" runat="server" CssClass="text-info fw-bold" /><br />
            
            <h5 class="text-secondary fw-bold">Búsqueda de Subvector (B):</h5>
            <div class="mb-3">
                <asp:Label ID="lblSubvectorSize" runat="server" Text="Tamaño del subvector B:" CssClass="form-label fw-semibold" />
                <asp:TextBox ID="txtSubvectorSize" runat="server" CssClass="form-control" />
            </div>
            <div class="d-grid mb-3">
                <asp:Button ID="btnGenerarSubvector" runat="server" Text="Generar Campos Subvector" CssClass="btn btn-info text-white" OnClick="btnGenerarSubvector_Click" />
            </div>
            <asp:Panel ID="panelSubvector" runat="server" CssClass="mb-3"></asp:Panel>
            <div class="d-grid mb-4">
    <asp:Button ID="btnGuardarSubvector" runat="server" Text="Guardar Subvector" CssClass="btn btn-success" OnClick="btnGuardarSubvector_Click" />
</div>
            <div class="d-grid mb-4">
                <asp:Button ID="btnBuscarSubvector" runat="server" Text="Buscar Subvector" CssClass="btn btn-dark" OnClick="btnBuscarSubvector_Click" />
            </div>
            
            <hr style="border-top: 2px solid #ccc;">

            <h5 class="text-secondary fw-bold">Producto Escalar (C):</h5>
            <div class="mb-3">
                <asp:Label ID="lblVector2Size" runat="server" Text="Tamaño del vector C:" CssClass="form-label fw-semibold" />
                <asp:TextBox ID="txtVector2Size" runat="server" CssClass="form-control" />
            </div>
            <div class="d-grid mb-3">
                <asp:Button ID="btnGenerarVector2" runat="server" Text="Generar Campos Vector C" CssClass="btn btn-info text-white" OnClick="btnGenerarVector2_Click" />
            </div>
            <asp:Panel ID="panelVector2" runat="server" CssClass="mb-3"></asp:Panel>
            <div class="d-grid mb-4">
                <asp:Button ID="btnProductoEscalar" runat="server" Text="Calcular Producto Escalar" CssClass="btn btn-dark" OnClick="btnProductoEscalar_Click" />
            </div>

            <hr style="border-top: 2px solid #ccc;">
            
            <h5 class="text-secondary fw-bold">Intercalar Vectores (D):</h5>
            <div class="mb-3">
                <asp:Label ID="lblVector3Size" runat="server" Text="Tamaño del vector D:" CssClass="form-label fw-semibold" />
                <asp:TextBox ID="txtVector3Size" runat="server" CssClass="form-control" />
            </div>
            <div class="d-grid mb-3">
                <asp:Button ID="btnGenerarVector3" runat="server" Text="Generar Campos Vector D" CssClass="btn btn-info text-white" OnClick="btnGenerarVector3_Click" />
            </div>
            <asp:Panel ID="panelVector3" runat="server" CssClass="mb-3"></asp:Panel>
            <div class="d-grid mb-4">
                <asp:Button ID="btnIntercalar" runat="server" Text="Intercalar Vectores" CssClass="btn btn-dark" OnClick="btnIntercalar_Click" />
            </div>

            <hr style="border-top: 2px solid #ccc;">
            
            <h5 class="text-secondary fw-bold">Fusión Ordenada (E):</h5>
            <div class="mb-3">
                <asp:Label ID="lblVector4Size" runat="server" Text="Tamaño del vector E:" CssClass="form-label fw-semibold" />
                <asp:TextBox ID="txtVector4Size" runat="server" CssClass="form-control" />
            </div>
            <div class="d-grid mb-3">
                <asp:Button ID="btnGenerarVector4" runat="server" Text="Generar Campos Vector E" CssClass="btn btn-info text-white" OnClick="btnGenerarVector4_Click" />
            </div>
            <asp:Panel ID="panelVector4" runat="server" CssClass="mb-3"></asp:Panel>
            <div class="d-grid mb-4">
                <asp:Button ID="btnFusionOrdenada" runat="server" Text="Fusionar y Ordenar" CssClass="btn btn-dark" OnClick="btnFusionOrdenada_Click" />
            </div>

            <hr style="border-top: 2px solid #ccc;">
            
            <h5 class="text-secondary fw-bold">Resultados:</h5>
            <div class="alert alert-info">
                <asp:Label ID="lblResultados" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>