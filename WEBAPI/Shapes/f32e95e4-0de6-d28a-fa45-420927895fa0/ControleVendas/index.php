<?php

require_once 'Functions/conexao.php';
require_once 'Functions/funcoes.class.php';
require_once 'Functions/crud.php';

$objFcn = new Registro;




    if(isset($_POST['salvar'])){       
    
        if($objFcn->queryInsert($_POST) == 'ok'){
            header('location: index.php');
            }else{
            echo '<script type="text/javascript">alert("PDV Cadastrado");</script>';
        }
    }
   
?>
<html>
    <head>
        <title>Controle</title>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width">
        
        <script type="text/javascript">
            function id(valor_campo){
                return document.getElementById(valor_campo);
            }
            function getValor(valor_campo){
                var valor = document.getElementById(valor_campo). value; 
                /*document.write("valor: " + valor);*/
                return parseFloat( valor ); 
            }
            
            function subtracao(){
                var total = getValor('n1') - getValor('n2'); 
                id('Sobra'). value = total; 
            }

            function id2(valor_campo){
                return document.getElementById(valor_campo);
            }
            function getValor2(valor_campo){
                var valor = document.getElementById(valor_campo). value; 
                /*document.write("valor: " + valor);*/
                return parseFloat( valor ) * 100; 
            }
            
            function soma(){
                var total = getValor2('n2') + getValor2('Sobra'); 
                id('Total_Real'). value = total/100; 
            }
        </script>
    <link rel="stylesheet" type="text/css" href="Style.css">
    </head>
    <body>

                   <form method="post" action="">
                        <label align="center"><h2>Controle de Chapas</h2></label>
                            <table border="1"> 
                                <tr>
                                    <td><label>Data</label></td>
                                    <td><label>PDV</label></td>
                                    <td><label>Cliente</label></td>
                                    <td><label>Chapa(as)</label></td>
                                    <td><label>Peso(PDV)</label></td>
                                    <td><label>Peso(REAL)</label></td>
                                    <td><label>Vendedor</label></td>
                                    <td><label>Cortador/Dobrador</label></td>
                                </tr>
                                <tr>
                                    <td><input type="text" name="Data"></td>
                                    <td><input type="text" name="PDV"></td>
                                    <td><input type="text" name="Cliente"></td>
                                    <td><input type="text" name="Chapa"></td>
                                    <td><input type="text" id="n1" name="Peso_PDV"></td>
                                    <td><input type="text" id="n2" name="Peso_Real" onblur="subtracao()"></td>
                                    <input type="hidden" name="Sobra" readonly="readonly" id="Sobra" /><br />
                                    <input type="hidden" name="Total_Real" readonly="readonly" id="Total_Real" /><br />
                                    <td><input type="text" name="Vendedor"></td>
                                    <td><input type="text" name="Corte"></td>
                                </tr>
                            </table>
                            <input type="submit" name="salvar" value="salvar" onclick="soma()" >
                   </form>                             

        <div>
        <div class="Botao_Responder">                   
                 <input value="Consultar PDVs" font-size="30px" onclick="if (this.parentNode.parentNode.getElementsByTagName('div')[1].getElementsByTagName('div')[0].style.display != '') { this.parentNode.parentNode.getElementsByTagName('div')[1].getElementsByTagName('div')[0].style.display = '';this.innerText = ''; this.value = 'Cancelar'; } else { this.parentNode.parentNode.getElementsByTagName('div')[1].getElementsByTagName('div')[0].style.display = 'none'; this.value = 'Consultar PDVs';}"input type="submit"  class="btn2 hover" >              
                    </div> 
                    <div><div class="spoiler" style="display: none;">
                        <form method="POST" action="pesquisar.php">
                            Pesquisar:<input type="text" name="pesquisar" placeholder="PESQUISAR">
                            <input type="submit" value="ENVIAR">
                        </form>                    
                        </div></div>    
            
        </div>
                <div>
        <div class="Botao_Responder">                   
                 <input value="Consultar Nº PDV" font-size="30px" onclick="if (this.parentNode.parentNode.getElementsByTagName('div')[1].getElementsByTagName('div')[0].style.display != '') { this.parentNode.parentNode.getElementsByTagName('div')[1].getElementsByTagName('div')[0].style.display = '';this.innerText = ''; this.value = 'Cancelar'; } else { this.parentNode.parentNode.getElementsByTagName('div')[1].getElementsByTagName('div')[0].style.display = 'none'; this.value = 'Consultar nº PDV';}"input type="submit"  class="btn2 hover" >              
                    </div> 
                    <div><div class="spoiler" style="display: none;">
                            <form method="POST" action="PesquisarPDV.php">
                            Pesquisar:<input type="text" name="pesquisar" placeholder="PESQUISAR">
                            <input type="submit" value="ENVIAR">
                        </form>                    
                        </div></div>    
            
        </div>
      
 </body>
</html>
