<?php 
include "dbConnection.php";

$userName = $_POST['userName'];
$pass          = hash("sha256" , $_POST['pass']);

$sql = "SELECT userName FROM usuarios WHERE userName = '$userName' AND password  = '$pass'"; 
$result = $pdo->query($sql);

if ($result->rowCount() > 0) 
{
    $data = array('done' => true, 'message' => "Bienvenido $userName"); 
    Header('Content-Type: application/json'); 
    echo json_encode($data);
    exit();
}
else 
{
    $data = array('done' => false, 'message' => "Usuario o contraseña incorrecto"); 
    Header('Content-Type: application/json'); 
    echo json_encode($data);
    exit();
}










?>