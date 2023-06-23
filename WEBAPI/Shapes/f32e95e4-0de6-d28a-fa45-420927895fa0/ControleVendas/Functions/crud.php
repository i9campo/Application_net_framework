<?php

include_once "conexao.php";
include_once "funcoes.class.php";

class Registro {
    
    private $con;
    private $objfc;
    private $Data;
    private $PDV;
    private $Cliente;
    private $Chapa; 
    private $Peso_PDV;
    private $Peso_Real;
    private $Sobra; 
    Private $Total_Real;
    private $Vendedor; 
    private $Corte; 
    

    public function __construct(){
        $this->con = new Conexao();
        $this->objfc = new Funcoes();
    }
    
    public function __set($atributo, $valor){
        $this->$atributo = $valor;
    }
    public function __get($atributo){
        return $this->$atributo;
    }
    
    public function querySeleciona($dado){
        try{
            $this-> Data = ($dado['Data']);
            $cst = $this->con->conectar()->prepare("SELECT  `controle` FROM `controle` WHERE `Data` = :data;");
            $cst->bindParam(":Data", $this->Data, PDO::PARAM_STR);
            $cst->execute();
            return $cst->fetch();
        } catch (PDOException $ex) {
            return 'error '.$ex->getMessage();
        }
    }
    
    public function querySelect($dado){
        try{
            
            $cst = $this->con->conectar()->prepare("SELECT  *FROM `controle` where `Data` = :Data  ;");
            $cst->execute();
            return $cst->fetchAll();
        } catch (PDOException $ex) {
            return 'erro '.$ex->getMessage();
        }
    }
    
    public function queryInsert($dados){ 
       try{
            $this->Data = ($dados['Data']);
            $this->PDV = ($dados['PDV']);
            $this->Cliente = $this->objfc->tratarCaracter($dados['Cliente'], 1);
            $this->Chapa = $this->objfc->tratarCaracter($dados['Chapa'], 1);
            $this->Peso_PDV = ($dados['Peso_PDV']);
            $this->Peso_Real = ($dados['Peso_Real']);
            $this->Sobra = ($dados['Sobra']);
            $this->Total_Real = ($dados['Total_Real']);
            $this->Vendedor = ($dados['Vendedor']); 
            $this->Corte = ($dados['Corte']);
            
            $cst = $this->con->conectar()->prepare("INSERT INTO `controle` (`Data` , `PDV`, `Cliente`, `Chapa`, `Peso_PDV`, `Peso_Real`, `Sobra`, `Total_Real`, `Vendedor`, `Corte`) VALUES (:Data, :PDV, :Cliente, :Chapa, :Peso_PDV, :Peso_Real, :Sobra, :Total_Real, :Vendedor, :Corte);");
            $cst->bindParam(":Data", $this->Data, PDO::PARAM_STR);
            $cst->bindParam(":PDV", $this->PDV, PDO::PARAM_INT);
            $cst->bindParam(":Cliente", $this->Cliente, PDO::PARAM_STR);            
            $cst->bindParam(":Chapa", $this->Chapa, PDO::PARAM_STR);
            $cst->bindParam(":Peso_PDV", $this->Peso_PDV, PDO::PARAM_INT);
            $cst->bindParam(":Peso_Real", $this->Peso_Real, PDO::PARAM_INT);
            $cst->bindParam(":Sobra", $this->Sobra, PDO::PARAM_INT);            
            $cst->bindParam(":Total_Real", $this->Total_Real, PDO::PARAM_INT);
            $cst->bindParam(":Vendedor", $this->Vendedor, PDO::PARAM_STR);            
            $cst->bindParam(":Corte", $this->Corte, PDO::PARAM_STR);
       
            
            if($cst->execute()){      
                return 'ok';
                }else{
                return 'erro';
            }
        } catch (PDOException $ex) {
            return 'error '.$ex->getMessage();
        }
    }
    
    public function queryUpdate($dados){
        try{
            $this->idFuncionario = $this->objfc->base64($dados['func'], 2);
            $this->nome = $this->objfc->tratarCaracter($dados['nome'], 1);
            $this->email = $dados['email'];
            $cst = $this->con->conectar()->prepare("UPDATE `funcionario` SET  `nome` = :nome, `email` = :email WHERE `idFuncionario` = :idFunc;");
            $cst->bindParam(":idFunc", $this->idFuncionario, PDO::PARAM_INT);
            $cst->bindParam(":nome", $this->nome, PDO::PARAM_STR);
            $cst->bindParam(":email", $this->email, PDO::PARAM_STR);
            if($cst->execute()){
                return 'ok';
            }else{
                return 'erro';
            }
        } catch (PDOException $ex) {
            return 'error '.$ex->getMessage();
        }
    }
    
    public function queryDelete($dado){
        try{
            $this->idFuncionario = $this->objfc->base64($dado, 2);
            $cst = $this->con->conectar()->prepare("DELETE FROM `funcionario` WHERE `idFuncionario` = :idFunc;");
            $cst->bindParam(":idFunc", $this->idFuncionario, PDO::PARAM_INT);
            if($cst->execute()){
                return 'ok';
            }else{
                return 'erro';
            }
        } catch (PDOException $ex) {
            return 'error'.$ex->getMessage();
        }
    }
 
    
    
 
        
        
        
}
?>
