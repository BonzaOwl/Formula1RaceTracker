<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manage-race-calendar.aspx.cs" Inherits="FormulaOneWebApp.administration.manage_race_calendar" %>

<!doctype html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="/docs/4.0/assets/img/favicons/favicon.ico">

    <title>F1 Race Tracker Admin</title>

    <link rel="canonical" href="https://getbootstrap.com/docs/4.0/examples/dashboard/">

    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
    <link href="/css/admin.css" rel="stylesheet" />
    <link href="../css/override.css" rel="stylesheet" />

</head>

<body>
    <nav class="navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0">
        <a class="navbar-brand col-sm-3 col-md-2 mr-0" href="#">F1 Tracker Admin</a>
        <ul class="navbar-nav px-3">
            <li class="nav-item text-nowrap"></li>
        </ul>
    </nav>

    <div class="container-fluid">
        <div class="row">
            <nav class="col-md-2 d-none d-md-block bg-light sidebar">
                <div class="sidebar-sticky">
                    <ul class="nav flex-column">
                        <li class="nav-item">
                            <a class="nav-link active" href="/index.aspx">
                                <span data-feather="home"></span>
                                Submit Results <span class="sr-only">(current)</span>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link active" href="#">
                                <span data-feather="home"></span>
                                Dashboard <span class="sr-only">(current)</span>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="add-driver.aspx">
                                <span data-feather="file"></span>
                                Add Driver
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="manage-drivers.aspx">
                                <span data-feather="shopping-cart"></span>
                                Manage Drivers
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">
                                <span data-feather="users"></span>
                                Add Circuit
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="manage-circuits.aspx">
                                <span data-feather="bar-chart-2"></span>
                                Manage Circuit
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="manage-race-calendar.aspx">
                                <span data-feather="layers"></span>
                                Manage Race Calendar
                            </a>
                        </li>
                    </ul>
                </div>
            </nav>

            <main role="main" class="col-md-9 ml-sm-auto col-lg-10 pt-3 px-4">
                <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pb-2 mb-3 border-bottom">
                    <h1 class="h2">Manage Circuits</h1>
                </div>

                <form runat="server">
                    <asp:GridView ID="x_list_all_race_dates" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Circuits Found">
                        <Columns>
                            <asp:BoundField HeaderText="ID" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="RaceDateID" />
                            <asp:BoundField HeaderText="Grands Prix Name" DataField="Grands_Prix_Name" />
                            <asp:BoundField HeaderText="Race Date" DataField="Race_Date" />
                            <asp:BoundField HeaderText="Race Type" DataField="Race_Type" />                            
                            <asp:TemplateField ItemStyle-Width="10%" HeaderStyle-Width="10%" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="x_edit_driver" Text="Edit Circuit" CssClass="btn btn-sm btn-success mb-2 mt-2" runat="server" />                                                                        
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </form>
            </main>
        </div>
    </div>

    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script>window.jQuery || document.write('<script src="../../assets/js/vendor/jquery-slim.min.js"><\/script>')</script>
    <script src="../../assets/js/vendor/popper.min.js"></script>
    <script src="../../dist/js/bootstrap.min.js"></script>

    <!-- Icons -->
    <script src="https://unpkg.com/feather-icons/dist/feather.min.js"></script>
    <script>
        feather.replace()
    </script>

</body>
</html>
