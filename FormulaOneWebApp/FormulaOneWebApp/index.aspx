<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" MasterPageFile="~/F1.Master" Inherits="FormulaOneWebApp.index" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" />

    <div class="container py-3">
        <div class="row">
            <div class="col-12 col-sm-8 col-md-6 mx-auto">
                <div id="submit-results" class="card">
                    <div class="card-body">
                        <div class="card-title">
                            <h2 class="text-center">Formula One Race Tracker</h2>
                        </div>

                        <p><strong>Written By:</strong> <a href="http://www.codenameowl.com" target="_blank">Bonza Owl</a></p>

                        <hr> 
                        
                        <div class="alert alert-info">
                            <asp:Label ID="x_next_race" runat="server"></asp:Label>                            
                        </div>

                        <asp:Button runat="server" ID="x_view_results" CssClass="btn btn-block btn-primary btn-sm" Text="View Results" OnClick="x_view_results_Click" />                        
                        <asp:Button runat="server" ID="x_admin" CssClass="btn btn-block btn-danger btn-sm" OnClick="x_admin_Click" Text="Tracker Admin" />

                        <hr>

                        <asp:Panel runat="server" ID="PanVerification" Visible="false">
                            <asp:Label runat="server" ID="x_state"></asp:Label>
                            <hr />
                        </asp:Panel>

                            <div class="row">
                                <div class="col-6">
                                    <div class="form-group">

                                        <label for="x_date" class="control-label">Race Date</label>
                                        <asp:TextBox runat="server" ID="x_date" placeholder="dd/mm/yyyy" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="x_date_extender" TargetControlID="x_date" Format="dd/MM/yyyy" runat="server" />

                                    </div>
                                </div>

                                <div class="col-6">
                                    <label for="x_driver_drop" class="control-label">Driver</label>
                                    <div class="input-group">
                                        <asp:DropDownList runat="server" ID="x_driver_drop" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-6">
                                    <div class="form-group">
                                        <label for="x_final_time" class="control-label">Race Type</label>
                                        <asp:DropDownList runat="server" ID="x_race_type_drop" CssClass="form-control" />
                                    </div>
                                </div>

                                <div class="col-6">
                                    <label for="x_position_drop" class="control-label">Circuit</label>
                                    <div class="form-group">
                                        <asp:DropDownList runat="server" ID="x_circuits_drop" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-6">
                                    <label for="x_position_drop" class="control-label">Retired</label>
                                    <asp:DropDownList ID="x_retired" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-6" style="padding-bottom: 10px">
                                    <label for="x_position_drop" class="control-label">Final Position</label>
                                    <div class="input-group">
                                        <asp:DropDownList runat="server" ID="x_position_drop" CssClass="form-control">
                                            <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                            <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                            <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                            <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                        <div class="row">
                            <div class="col-md-6"></div>
                            <div class="col-md-6">
                                 <div id="fastest-lap">
                                        <label for="x_fastest_lap_chk" style="display:inline-block" class="control-label">Fastest Lap?</label>
                                        <asp:CheckBox ID="x_fastest_lap_chk" style="display:inline-block" runat="server" CssClass="form-check" />
                                    </div>
                            </div>
                        </div>

                            <div class="row">
                                <div class="col-6">
                                    <asp:Button runat="server" ID="x_save_btn" OnClick="x_save_btn_Click" CssClass="btn btn-lg btn-success btn-block" Text="Save" />

                                </div>

                                <div class="col-6">
                                    <asp:Button runat="server" ID="x_clear_btn" OnClick="x_clear_btn_Click" CssClass="btn btn-lg btn-primary btn-block" Text="Clear Form" />
                                </div>
                            </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>