<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="results.aspx.cs" Inherits="FormulaOneWebApp.results" %>

<!doctype html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <title>Formula One Race Tracker</title>

    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">

    <style>
        p {
            text-align: center;
        }
    </style>

</head>

<form id="form1" runat="server">

    <body class="bg-light">

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

                            <asp:Button runat="server" ID="x_results_btn" CssClass="btn btn-block btn-primary btn-sm" OnClick="x_results_btn_Click" Text="Submit Results" />
                                
                            <hr>

                            <asp:GridView runat="server" ID="x_race_results" CssClass="table table-bordered" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField HeaderText="Driver Name" DataField="DriverName" />
                                    <asp:BoundField HeaderText="Team Name" DataField="Team_Name" />
                                    <asp:BoundField HeaderText="Total Points" DataField="Points" />

                                </Columns>
                            </asp:GridView>                        

                        </div>

                    </div>

                </div>

            </div>

        </div>
    </body>
</form>
</html>
