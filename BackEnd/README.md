# Instrucciones para la Base de Datos del Backend

Este archivo contiene las instrucciones para configurar manualmente la base de datos MySQL necesaria para el backend del proyecto.

## Crear la Base de Datos

Conéctate a MySQL y ejecuta los siguientes comandos para crear la base de datos y las tablas necesarias.

```sql
CREATE DATABASE BackEnd;
USE BackEnd;

CREATE TABLE `order` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    customer_id INT NOT NULL,
    order_date DATETIME NOT NULL,
    delivery_date DATETIME NOT NULL,
    payment_method VARCHAR(50) NOT NULL,
    total_amount DECIMAL(10, 2) NOT NULL,
    status VARCHAR(50) NOT NULL,
    preferences_json TEXT,
    details_shown BOOLEAN NOT NULL
);

CREATE TABLE user_profiles (
    id INT AUTO_INCREMENT PRIMARY KEY,
    photo VARCHAR(255) NOT NULL,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    birth_date DATE NOT NULL,
    address TEXT NOT NULL,
    paymentmethod VARCHAR(50) NOT NULL,
    cardnumber VARCHAR(20) NOT NULL,
    yapenumber VARCHAR(20) NOT NULL,
    cashpayment BOOLEAN NOT NULL,
    preferencesJson TEXT
);

CREATE TABLE dishes (
    id INT AUTO_INCREMENT PRIMARY KEY,
    chef_name VARCHAR(100) NOT NULL,
    name_of_dish VARCHAR(100) NOT NULL,
    ingredients_json TEXT NOT NULL,
    preparation_steps_json TEXT NOT NULL,
    favorite BOOLEAN NOT NULL
);

CREATE TABLE chefs (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    rating DOUBLE NOT NULL,
    favorite BOOLEAN NOT NULL,
    gender VARCHAR(10) NOT NULL
);
```

## Configuración de Conexión a la Base de Datos

Para que la aplicación se conecte correctamente a la base de datos, es necesario configurar el archivo appsettings.json con la cadena de conexión adecuada.

Abre el archivo appsettings.json en el directorio raíz de tu proyecto backend.
Ubica la sección "ConnectionStrings".
Modifica la cadena de conexión para reflejar tus credenciales de MySQL. En el campo "Password", reemplaza con tu contraseña de MySQL Workbench.
Ejemplo de configuración en appsettings.json:

```json
{
"ConnectionStrings": {
"DefaultConnection": "Server=localhost;Database=catch-up-platform-ws51;User=root;Password=TU_CONTRASEÑA;"
},
"Logging": {
"LogLevel": {
"Default": "Information",
"Microsoft.AspNetCore": "Warning"
}
},
"AllowedHosts": "*"
}
```

Nota: Asegúrate de reemplazar TU_CONTRASEÑA con tu contraseña real de MySQL.