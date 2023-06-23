<?php

  

	$servidor = "localhost";
	$usuario = "root";
	$senha = "";
	$dbname = "controle";
	//Criar a conexao
	$conn = mysqli_connect($servidor, $usuario, $senha, $dbname);
	$count = 0; 
	$pesquisar = $_POST['pesquisar'];
	$result_cursos = "SELECT * FROM controle WHERE data LIKE '%$pesquisar%' LIMIT 5";
	$resultado_cursos = mysqli_query($conn, $result_cursos);
                
?>


<html>
    <head>
        <style>
            p{
                color: white; 
                text-align: center; 
                         
            }
            td{
                font-size: 17px; 
                text-align: center; 
            }
            h2{
                color: white; 
                
            }
        </style>
    </head>
    <body>
       <table border="1" align="center">
           <tr>
               <td colspan="10"  align="center" bgcolor="#696969"><h2>Controle de Chapas</h2></td>
           </tr>
           <tr>
               <td bgcolor="#000000"><p>Data</p></td>
               <td bgcolor="#000000"><p>PDV</p></td>
               <td bgcolor="#000000"><p>Cliente</p></td>
               <td bgcolor="#000000"><p>Chapa</p></td>
               <td bgcolor="#000000"><p>Peso Chapa</p></td>
               <td bgcolor="#000000"><p>Peso Real</p></td>
               <td bgcolor="#000000"><p>Sobra</p></td>
               <td bgcolor="#000000"><p>Total Real</p></td>
               <td bgcolor="#000000"><p>Vendedor</p></td>
               <td bgcolor="#000000"><p>Corte</p></td>
           </tr>   
        <?php 
            while($rows_cursos = mysqli_fetch_array($resultado_cursos)){
        ?>  
        <tr>   
            <td width="110"> <?php echo $rows_cursos['Data']; ?> </td>
            <td width="110"> <?php echo $rows_cursos['PDV']; ?> </td>
            <td width="250"> <?php echo $rows_cursos['Cliente']; ?> </td>
            <td width="130"> <?php echo $rows_cursos['Chapa']; ?> </td> 
            <td width="100"> <?php echo $rows_cursos['Peso_PDV']; ?> </td>
            <td width="100"> <?php echo $rows_cursos['Peso_Real']; ?> </td>
            <td width="100" bgcolor="#D3D3D3"> <?php echo $rows_cursos['Sobra']; ?> </td>
            <td width="100"> <?php echo $rows_cursos['Total_Real']; ?> </td>
            <td width="130"> <?php echo $rows_cursos['Vendedor']; ?> </td>
            <td width="130"> <?php echo $rows_cursos['Corte']; "<br />" ?></td>      
        </tr>
        <?php
            }
        ?>
        </table>
        
    </body>
</html>