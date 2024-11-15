<?php 

include "dbConnection.php";

$userName    = $_POST['userName'];
$email            = $_POST['email'];
$pass              = hash("sha256" , $_POST['pass']);

$sql = "SELECT userName FROM usuarios WHERE userName = 'userName'";
$result = $pdo->query($sql);


if ($result->rowCount() > 0 ) 
{
    $data = array('done' => false, 'message' => "404 error usuario existente"); 
    Header('Content-Type: application/json'); 
    echo json_encode($data);
    exit();
}
else 
{   
    $sql = "SELECT email FROM usuarios WHERE email = 'email'";
    $result = $pdo->query($sql);

        if ($result->rowCount() > 0 ) 
    {
        $data = array('done' => false, 'message' => "404 error email existente"); 
        Header('Content-Type: application/json'); 
        echo json_encode($data);
        exit();
    }
    else 
    {
        $sql = "INSERT INTO usuarios SET userName = '$userName' , email = '$email' , password = '$pass'"; 
        $pdo->query($sql); 

        $data = array('done' => true, 'message' => "Creado con exito :D"); 
        Header('Content-Type: application/json'); 
        echo json_encode($data);
        exit();

    }
}

?>