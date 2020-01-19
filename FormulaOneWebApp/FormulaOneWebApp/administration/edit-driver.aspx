<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit-driver.aspx.cs" Inherits="FormulaOneWebApp.administration.edit_driver" %>

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

    <!-- Bootstrap core CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">

    <!-- Custom styles for this template -->
    <link href="../css/admin.css" rel="stylesheet" />

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
                                Submit Results<span class="sr-only">(current)</span>
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
                    <h1 class="h2">Edit Driver</h1>
                </div>

                <form runat="server">
                    <div class="row">
                        <div class="col-md-12 order-md-1">
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <label for="x_driver_first_name">First name</label>
                                    <asp:TextBox ID="x_driver_first_name" runat="server" CssClass="form-control"></asp:TextBox>
                                    <div class="invalid-feedback">
                                        Valid first name is required.
                                    </div>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <label for="x_driver_last_name">Last name</label>
                                    <asp:TextBox ID="x_driver_last_name" runat="server" CssClass="form-control"></asp:TextBox>
                                    <div class="invalid-feedback">
                                        Valid last name is required.
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <label for="x_driver_nationality">Nationality</label>
                                    <asp:DropDownList ID="x_driver_nationality" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <div class="invalid-feedback">
                                        Valid first name is required.
                                    </div>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <label for="x_driver_team">Team</label>
                                    <asp:DropDownList ID="x_driver_team" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <div class="invalid-feedback">
                                        Valid last name is required.
                                    </div>
                                </div>
                            </div>

                            <hr class="mb-4">
                            <asp:Button ID="x_save_btn" runat="server" OnClick="x_save_btn_Click" CssClass="btn btn-primary btn-lg btn-block" Text="Save Details" />
                        </div>
                    </div>

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
