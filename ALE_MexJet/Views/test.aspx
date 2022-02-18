<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="ALE_MexJet.Views.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/prettify.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="modal-footer">
            <asp:Button ID="btnTest" runat="server" Text=":-:" CssClass="btn btn-default" OnClick="btnTest_Click" />
            <asp:Button ID="btnCredencial" runat="server" Text="..:-:.." Visible="false" OnClick="btnCredencial_Click" />
        </div>
    </div>
    </form>
    <div class='container'>

				<div id="rootwizard">
					<div class="navbar">
					  <div class="navbar-inner">
					    <div class="container">
					<ul>
					  	<li><a href="#tab1" data-toggle="tab">First</a></li>
						<li><a href="#tab2" data-toggle="tab">Second</a></li>
						<li><a href="#tab3" data-toggle="tab">Third</a></li>
						<li><a href="#tab4" data-toggle="tab">Forth</a></li>
						<li><a href="#tab5" data-toggle="tab">Fifth</a></li>
						<li><a href="#tab6" data-toggle="tab">Sixth</a></li>
						<li><a href="#tab7" data-toggle="tab">Seventh</a></li>
					</ul>
					 </div>
					  </div>
					</div>
					<div class="tab-content">
					    <div class="tab-pane" id="tab1">
					      1
					    </div>
					    <div class="tab-pane" id="tab2">
					      2
					    </div>
						<div class="tab-pane" id="tab3">
							3
					    </div>
						<div class="tab-pane" id="tab4">
							4
					    </div>
						<div class="tab-pane" id="tab5">
							5
					    </div>
						<div class="tab-pane" id="tab6">
							6
					    </div>
						<div class="tab-pane" id="tab7">
							7
					    </div>
						<ul class="pager wizard">
							<li class="previous first" style="display:none;"><a href="#">First</a></li>
							<li class="previous"><a href="#">Previous</a></li>
							<li class="next last" style="display:none;"><a href="#">Last</a></li>
						  	<li class="next"><a href="#">Next</a></li>
						</ul>
					</div>
				</div>
                </div>
                <!-- wizard2 -->
    <div class='container'>

        <section id="wizard">
            <div class="page-header">
                <h1>Wizard 2</h1>
            </div>

            <div id="rootwizard2">
                <div class="navbar">
                    <div class="navbar-inner">
                        <div class="container">
                            <ul>
                                <li><a href="#tab1a" data-toggle="tab">a</a></li>
                                <li><a href="#tab2a" data-toggle="tab">b</a></li>
                                <li><a href="#tab3a" data-toggle="tab">c</a></li>
                                <li><a href="#tab4a" data-toggle="tab">d</a></li>
                                <li><a href="#tab5a" data-toggle="tab">e</a></li>
                                <li><a href="#tab6a" data-toggle="tab">f</a></li>
                                <li><a href="#tab7a" data-toggle="tab">g</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="tab-content">
                    <div class="tab-pane" id="tab1a">
                        a
                    </div>
                    <div class="tab-pane" id="tab2a">
                       b
                    </div>
                    <div class="tab-pane" id="tab3a">
                        c
                    </div>
                    <div class="tab-pane" id="tab4a">
                        d
                    </div>
                    <div class="tab-pane" id="tab5a">
                        e
                    </div>
                    <div class="tab-pane" id="tab6a">
                        f
                    </div>
                    <div class="tab-pane" id="tab7a">
                        g
                    </div>
                    <ul class="pager wizard">
                        <li class="previous first" style="display: none;"><a href="#">First</a></li>
                        <li class="previous"><a href="#">Previous</a></li>
                        <li class="next last" style="display: none;"><a href="#">Last</a></li>
                        <li class="next"><a href="#">Next</a></li>
                    </ul>
                </div>
            </div>
            <!-- wizard2 -->
    <!-- tabs -->
    <br /><br /><br />
<div class="bs-example bs-example-tabs" role="tabpanel" data-example-id="togglable-tabs">
    <ul id="myTab" class="nav nav-tabs" role="tablist">
      <li role="presentation" class="active"><a href="#home" id="home-tab" role="tab" data-toggle="tab" aria-controls="home" aria-expanded="true">Home</a></li>
      <li role="presentation" class=""><a href="#profile" role="tab" id="profile-tab" data-toggle="tab" aria-controls="profile" aria-expanded="false">Profile</a></li>
      <li role="presentation" class="dropdown">
        <a href="#" id="myTabDrop1" class="dropdown-toggle" data-toggle="dropdown" aria-controls="myTabDrop1-contents" aria-expanded="false">Dropdown <span class="caret"></span></a>
        <ul class="dropdown-menu" role="menu" aria-labelledby="myTabDrop1" id="myTabDrop1-contents">
          <li><a href="#dropdown1" tabindex="-1" role="tab" id="dropdown1-tab" data-toggle="tab" aria-controls="dropdown1">@fat</a></li>
          <li><a href="#dropdown2" tabindex="-1" role="tab" id="dropdown2-tab" data-toggle="tab" aria-controls="dropdown2">@mdo</a></li>
        </ul>
      </li>
    </ul>
    <div id="myTabContent" class="tab-content">
      <div role="tabpanel" class="tab-pane fade active in" id="home" aria-labelledby="home-tab">
        <p>Raw denim you probably haven't heard of them jean shorts Austin. Nesciunt tofu stumptown aliqua, retro synth master cleanse. Mustache cliche tempor, williamsburg carles vegan helvetica. Reprehenderit butcher retro keffiyeh dreamcatcher synth. Cosby sweater eu banh mi, qui irure terry richardson ex squid. Aliquip placeat salvia cillum iphone. Seitan aliquip quis cardigan american apparel, butcher voluptate nisi qui.</p>
      </div>
      <div role="tabpanel" class="tab-pane fade" id="profile" aria-labelledby="profile-tab">
        <p>Food truck fixie locavore, accusamus mcsweeney's marfa nulla single-origin coffee squid. Exercitation +1 labore velit, blog sartorial PBR leggings next level wes anderson artisan four loko farm-to-table craft beer twee. Qui photo booth letterpress, commodo enim craft beer mlkshk aliquip jean shorts ullamco ad vinyl cillum PBR. Homo nostrud organic, assumenda labore aesthetic magna delectus mollit. Keytar helvetica VHS salvia yr, vero magna velit sapiente labore stumptown. Vegan fanny pack odio cillum wes anderson 8-bit, sustainable jean shorts beard ut DIY ethical culpa terry richardson biodiesel. Art party scenester stumptown, tumblr butcher vero sint qui sapiente accusamus tattooed echo park.</p>
      </div>
      <div role="tabpanel" class="tab-pane fade" id="dropdown1" aria-labelledby="dropdown1-tab">
        <p>Etsy mixtape wayfarers, ethical wes anderson tofu before they sold out mcsweeney's organic lomo retro fanny pack lo-fi farm-to-table readymade. Messenger bag gentrify pitchfork tattooed craft beer, iphone skateboard locavore carles etsy salvia banksy hoodie helvetica. DIY synth PBR banksy irony. Leggings gentrify squid 8-bit cred pitchfork. Williamsburg banh mi whatever gluten-free, carles pitchfork biodiesel fixie etsy retro mlkshk vice blog. Scenester cred you probably haven't heard of them, vinyl craft beer blog stumptown. Pitchfork sustainable tofu synth chambray yr.</p>
      </div>
      <div role="tabpanel" class="tab-pane fade" id="dropdown2" aria-labelledby="dropdown2-tab">
        <p>Trust fund seitan letterpress, keytar raw denim keffiyeh etsy art party before they sold out master cleanse gluten-free squid scenester freegan cosby sweater. Fanny pack portland seitan DIY, art party locavore wolf cliche high life echo park Austin. Cred vinyl keffiyeh DIY salvia PBR, banh mi before they sold out farm-to-table VHS viral locavore cosby sweater. Lomo wolf viral, mustache readymade thundercats keffiyeh craft beer marfa ethical. Wolf salvia freegan, sartorial keffiyeh echo park vegan.</p>
      </div>
    </div>
  </div><br />
<!-- tabs -->
             <!-- Ventana modal -->
        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" data-whatever="">Abrir modal</button>

        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h3>Título</h3>
                    </div>
                    <div class="modal-body">

                        <d<!-- wizard3 -->
    <div class='container'>

        <section id="wizard">
            <div class="page-header">
                <h1>Wizard 3 en modal</h1>
            </div>

            <div id="rootwizard3">
                <div class="navbar">
                    <div class="navbar-inner">
                        <div class="container">
                            <ul>
                                <li><a href="#tab1ab" data-toggle="tab">1a</a></li>
                                <li><a href="#tab2ab" data-toggle="tab">2b</a></li>
                                <li><a href="#tab3ab" data-toggle="tab">3c</a></li>
                                <li><a href="#tab4ab" data-toggle="tab">4d</a></li>
                                <li><a href="#tab5ab" data-toggle="tab">5e</a></li>
                                <li><a href="#tab6ab" data-toggle="tab">6f</a></li>
                                <li><a href="#tab7ab" data-toggle="tab">7g</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="tab-content">
                    <div class="tab-pane" id="tab1ab">
                        1a
                    </div>
                    <div class="tab-pane" id="tab2ab">
                       2b
                    </div>
                    <div class="tab-pane" id="tab3ab">
                        3c
                    </div>
                    <div class="tab-pane" id="tab4ab">
                        4d
                    </div>
                    <div class="tab-pane" id="tab5ab">
                        5e
                    </div>
                    <div class="tab-pane" id="tab6ab">
                        6f
                    </div>
                    <div class="tab-pane" id="tab7ab">
                        7g
                    </div>
                    <ul class="pager wizard">
                        <li class="previous first" style="display: none;"><a href="#">First</a></li>
                        <li class="previous"><a href="#">Previous</a></li>
                        <li class="next last" style="display: none;"><a href="#">Last</a></li>
                        <li class="next"><a href="#">Next</a></li>
                    </ul>
                </div>
            </div>
            <!-- wizard3 -->


                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                    
                </div>
            </div>
        </div>
        <!-- Ventana modal -->
       <script src="http://code.jquery.com/jquery-latest.js"></script>
    <script src="../Js/bootstrap.min.js"></script>
    <script src="../Js/jquery.bootstrap.wizard.js"></script>
    <script src="../Js/prettify.js"></script>
	<script>
	    $(document).ready(function () {
	        $('#rootwizard').bootstrapWizard();
	        window.prettyPrint && prettyPrint()
	    });
	</script>
    <script>
            	    $(document).ready(function () {
            	        $('#rootwizard2').bootstrapWizard();
            	        window.prettyPrint && prettyPrint()
            	    });
	</script>
            <script>
                $(document).ready(function () {
                    $('#rootwizard3').bootstrapWizard();
                    window.prettyPrint && prettyPrint()
                });
	</script>
</body>
</html>
