<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AspxWebApplication._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

  <div class="row">
    <div class="col-md-12 text-center">
      <h1 class="display-4">Welcome to IronPdf sample</h1>
      <p>Learn about <a href="https://ironpdf.com/docs/">using IronPdf with ASP.NET Core</a>.</p>
    </div>
  </div>

  <div class="row">
    <div class="col-md-offset-2 col-md-8">
      <h2>Render PDF by url</h2>
      <p>
        Use the button "Render this page" to render a PDF document by the current web page.
      </p>
      <a runat="server" class="btn btn-lg btn-primary" target="_blank" href="~/Default?pdfAttachment=true">Render this page</a>
    </div>
  </div>

</asp:Content>
