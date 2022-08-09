<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WordServiceGui._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>WordFilter TryIt - Remove stop words from a string</h1>
        <p>Enter a string of words:</p>
        <p class="lead">
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="Remove Stop Words" />
        </p>
        <p>Output:
            <asp:Label ID="output" runat="server" Text=" "></asp:Label>
            <asp:Label ID="Label1" runat="server" Text=" "></asp:Label>
        </p>
        <hr />
        <h1>Top10Words TryIt - Find the most used words on a page</h1>
        <p>Enter a url:</p>
        <p>
            <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Find Top 10 Words" />
        </p>
        <p>Output:
            <asp:Label ID="output0" runat="server" Text=" "></asp:Label>
            <asp:Label ID="Label2" runat="server" Text=" "></asp:Label>
        </p>
        <hr />
        <h1>WordNet TryIt - Find &#39;parent&#39; noun of two &#39;child&#39; nouns</h1>
        <p>Enter 2 nouns:</p>
        <p>
            <asp:TextBox ID="word1" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
        </p>
        <p>
            <asp:TextBox ID="word2" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Lowest Common Ancester" />
        </p>
        <p>
            Output:
            <asp:Label ID="LCAlabel" runat="server" Text=" "></asp:Label>
        </p>
    </div>

    

</asp:Content>
