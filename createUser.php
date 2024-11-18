<?php 
include "dbConnection.php";

$nombre         = $_POST['nombre'];
$apellido         = $_POST['apellido'];
$edad              = $_POST['edad'];
$usuario          = $_POST['usuario'];
$correo            = $_POST['correo'];
$contraseña     = hash("sha256" , $_POST['contraseña'] );

$sql = "SELECT id_usuario FROM estudiante WHERE id_usuario = '$usuario' ";
$result = $pdo->query($sql); 

if ($result->rowCount() > 0) 
{
    $data= array('done' => false , 'message' => "Error usuario ya existente");
    Header('Content-Type: application/json'); 
    echo json_encode($data);
    exit();
}
else 
{
    $sql = "SELECT correo FROM estudiante WHERE correo = '$correo' ";
    $result = $pdo->query($sql); 

    if ($result->rowCount() > 0) 
{
    $data= array('done' => false , 'message' => "Error correo ya existente");
    Header('Content-Type: application/json'); 
    echo json_encode($data);
    exit();
}
else 
{
    $sql = "INSERT INTO estudiante SET nombre = '$nombre' , apellido = '$apellido' , edad = '$edad' , correo = '$correo' , contraseña = '$contraseña' , id_usuario = '$usuario' , id_cargo = 1";
    $pdo->query($sql); 

    $data= array('done' => true , 'message' => "Usuario Creado");
    Header('Content-Type: application/json'); 
    echo json_encode($data);
    exit();
}

}

?>