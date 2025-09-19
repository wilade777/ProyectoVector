<%@ Page Title="Transformación de Vectores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransformacionAvanzada.aspx.cs" Inherits="Proyecto.Vector.Presentacion.TransformacionAvanzada" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="card shadow-lg p-4 rounded-3" style="max-width: 900px; margin: auto; background-color: #f9f9f9;">
            <h2 class="text-center mb-4 text-primary fw-bold">Transformación y Manipulación de Vectores</h2>

            <h5 class="text-secondary fw-bold">Vector Principal (A):</h5>
            <div class="alert alert-secondary">
                <asp:Label ID="lblVectorA" runat="server" Text="Vector no cargado. Por favor, cargue uno en la página principal." />
            </div>

            <!-- Rotación Circular -->
            <div class="vector-operation mb-4">
                <h5 class="text-secondary fw-bold">Rotación Circular:</h5>
                <div class="row mb-3">
                    <div class="col-md-6">
                        <asp:Label runat="server" Text="Posiciones a rotar:" CssClass="form-label fw-semibold" />
                        <asp:TextBox ID="txtRotaciones" runat="server" CssClass="form-control" TextMode="Number" value="1" />
                    </div>
                </div>
                <div class="d-grid">
                    <asp:Button ID="btnRotarDerecha" runat="server" Text="Rotar a la Derecha" 
                        CssClass="btn btn-info mb-2" OnClick="btnRotarDerecha_Click" />
                    <asp:Button ID="btnRotarIzquierda" runat="server" Text="Rotar a la Izquierda" 
                        CssClass="btn btn-info" OnClick="btnRotarIzquierda_Click" />
                </div>
            </div>

            <!-- Eliminar Duplicados -->
            <div class="vector-operation mb-4">
                <h5 class="text-secondary fw-bold">Eliminar Duplicados:</h5>
                <div class="d-grid">
                    <asp:Button ID="btnEliminarDuplicados" runat="server" Text="Eliminar Duplicados" 
                        CssClass="btn btn-warning" OnClick="btnEliminarDuplicados_Click" />
                </div>
            </div>

            <!-- Ordenar Pares e Impares -->
            <div class="vector-operation mb-4">
                <h5 class="text-secondary fw-bold">Ordenar Pares e Impares:</h5>
                <div class="d-grid">
                    <asp:Button ID="btnOrdenarParesImpares" runat="server" Text="Ordenar Pares e Impares" 
                        CssClass="btn btn-success" OnClick="btnOrdenarParesImpares_Click" />
                </div>
            </div>

            <!-- Inversión Parcial -->
            <div class="vector-operation mb-4">
                <h5 class="text-secondary fw-bold">Inversión Parcial:</h5>
                <div class="row mb-3">
                    <div class="col-md-6">
                        <asp:Label runat="server" Text="Índice de inicio:" CssClass="form-label fw-semibold" />
                        <asp:TextBox ID="txtInicioInversion" runat="server" CssClass="form-control" TextMode="Number" value="0" />
                    </div>
                    <div class="col-md-6">
                        <asp:Label runat="server" Text="Índice de fin:" CssClass="form-label fw-semibold" />
                        <asp:TextBox ID="txtFinInversion" runat="server" CssClass="form-control" TextMode="Number" value="0" />
                    </div>
                </div>
                <div class="d-grid">
                    <asp:Button ID="btnInvertirParcial" runat="server" Text="Invertir Rango" 
                        CssClass="btn btn-danger" OnClick="btnInvertirParcial_Click" />
                </div>
            </div>

            <!-- Compactación por Condición -->
            <div class="vector-operation mb-4">
                <h5 class="text-secondary fw-bold">Compactación por Condición:</h5>
                <div class="row mb-3">
                    <div class="col-md-6">
                        <asp:Label runat="server" Text="Seleccione condición:" CssClass="form-label fw-semibold" />
                        <asp:DropDownList ID="ddlCondicion" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Números Pares" Value="pares" />
                            <asp:ListItem Text="Números Impares" Value="impares" />
                            <asp:ListItem Text="Números Positivos" Value="positivos" />
                            <asp:ListItem Text="Números Negativos" Value="negativos" />
                            <asp:ListItem Text="Números Primos" Value="primos" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="d-grid">
                    <asp:Button ID="btnCompactar" runat="server" Text="Compactar Vector" 
                        CssClass="btn btn-primary" OnClick="btnCompactar_Click" />
                </div>
            </div>

            <!-- Resultados -->
            <hr style="border-top: 2px solid #ccc;">
            <h5 class="text-secondary fw-bold">Resultados:</h5>
            <div class="alert alert-info">
                <asp:Label ID="lblResultados" runat="server" Text=""></asp:Label>
            </div>

            <!-- Vector Resultante -->
            <h5 class="text-secondary fw-bold">Vector Resultante:</h5>
            <div class="alert alert-success">
                <asp:Label ID="lblVectorResultante" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>

    <style>
        .vector-operation {
            padding: 15px;
            border-left: 4px solid #0d6efd;
            background-color: #fff;
            border-radius: 5px;
            margin-bottom: 20px;
        }
    </style>
</asp:Content>