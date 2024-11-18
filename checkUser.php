<?php 
include "dbConnection.php";

$correo            = $_POST['correo'];
$contraseña     = hash("sha256" , $_POST['contraseña'] );

$sql = "SELECT correo FROM estudiante WHERE correo = '$correo' AND contraseña = '$contraseña'";
$result = $pdo->query($sql); 

if ($result->rowCount() > 0 )
{
    $data= array('done' => true , 'message' => "Bienvenido");
    Header('Content-Type: application/json'); 
    echo json_encode($data);
    exit();
}  
else 
{
    $data= array('done' => false , 'message' => "usuario no encontrado");
    Header('Content-Type: application/json'); 
    echo json_encode($data);
    exit();
}

?>