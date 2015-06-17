﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASPPatterns.Chap4.DomainModel.UI.Web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Domain Model</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset>
                <legend>Create New Account</legend>
                <p>
                    Customer Ref:
                    <asp:TextBox ID="txtCustomerRef" runat="server" />
                    <asp:Button ID="btCreateAccount" runat="server" Text="Create Account"
                        OnClick="btCreateAccount_Click" />
                </p>
            </fieldset>
            <fieldset>
                <legend>Account Detail</legend>
                <p>
                    <asp:DropDownList AutoPostBack="true"
                        ID="ddlBankAccounts" runat="server"
                        OnSelectedIndexChanged="ddlBankAccounts_SelectedIndexChanged" />
                </p>
                <p>
                    Account No:
                    <asp:Label ID="lblAccountNo" runat="server" />
                </p>
                <p>
                    Customer Ref:
                    <asp:Label ID="lblCustomerRef" runat="server" />
                </p>
                <p>
                    Balance:
                    <asp:Label ID="lblBalance" runat="server" />
                </p>
                <p>
                    Amount £<asp:TextBox ID="txtAmount" runat="server" Width="60px" />
                    &nbsp;
                    <asp:Button ID="btnWithdrawal" runat="server" Text="Withdrawal"
                        OnClick="btnWithdrawal_Click" />
                    &nbsp;
                    <asp:Button ID="btnDeposit" runat="server" Text="Deposit"
                        OnClick="btnDeposit_Click" />
                </p>
                <p>
                    Transfer
£<asp:TextBox ID="txtAmountToTransfer" runat="server"
    Width="60px" />
                    &nbsp;to
                    <asp:DropDownList AutoPostBack="true"
                        ID="ddlBankAccountsToTransferTo" runat="server" />
                    &nbsp;
                    <asp:Button ID="btnTransfer" runat="server" Text="Commit"
                        OnClick="btnTransfer_Click" />
                </p>
                <p>
                    Transactions
                </p>
                <asp:Repeater ID="rptTransactions" runat="server">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td>deposit</td>
                                <td>withdrawal</td>
                                <td>reference</td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Deposit") %></td>
                            <td><%# Eval("Withdrawal") %></td>
                            <td><%# Eval("Reference") %></td>
                            <td><%# Eval("Date") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </fieldset>
        </div>
    </form>
</body>
</html>
