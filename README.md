# Sistema de Impresión de Documentos

Este proyecto implementa un sistema de impresión de documentos utilizando .NET Core 8, MassTransit y RabbitMQ para el manejo de colas de prioridad.

## Tabla de Contenidos

1. [Requisitos](#requisitos)
2. [Configuración](#configuración)
3. [Estructura del Proyecto](#estructura-del-proyecto)
4. [Uso](#uso)
5. [Docker y RabbitMQ](#docker-y-rabbitmq)
6. [Solución de Problemas](#solución-de-problemas)

## Requisitos

- .NET Core SDK 8.0 o superior
- Docker Desktop
- Visual Studio 2022 o VS Code (opcional)

## Configuración

1. Clonar el repositorio:
   ```
   git clone https://github.com/Nickoro/Nicolas_Kinder_TFI_P1.git
   ```

2. Navegar al directorio del proyecto:
   ```
   cd /Nicolas_Kinder_TFI_P1
   ```

3. Restaurar las dependencias:
   ```
   dotnet restore
   ```

4. Configurar RabbitMQ (ver sección [Docker y RabbitMQ](#docker-y-rabbitmq))

5. Actualizar la cadena de conexión en `appsettings.json` si es necesario.

## Estructura del Proyecto

- `Controllers/`: Contiene los controladores de la API.
- `Aplication/`: Servicios de aplicación y Consumidores, incluyendo `PrintingService`.
- `Domain/`: Clases de dominio.
- `Shared/`: DTOs.
- `Repositories/`: Contexto de base de datos y repositorios.

## Uso

1. Ejecutar la aplicación:  

2. La API estará disponible en `https://localhost:7007` (o el puerto configurado).

3. Utilizar Swagger UI para probar los endpoints: `https://localhost:7007/swagger`

### Endpoints principales:

- POST `/api/Printing/print`: Envía un documento para imprimir.
- GET `/api/Printing/status/{documentName}`: Verifica el estado de un documento.

## Docker y RabbitMQ

Este proyecto utiliza RabbitMQ en Docker para el manejo de colas.

### Configuración de RabbitMQ:

1. Iniciar RabbitMQ en Docker:
   ```
   docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3.9-management
   ```

2. Acceder a la interfaz de administración de RabbitMQ:
   - URL: `http://localhost:15672`
   - Usuario: guest
   - Contraseña: guest

### Gestión de RabbitMQ:

- Listar contenedores: `docker ps`
- Detener RabbitMQ: `docker stop some-rabbit`
- Iniciar RabbitMQ: `docker start some-rabbit`
- Ver logs: `docker logs some-rabbit`

### Eliminar cola:

Si necesitas eliminar la cola 'print-requests':
```
docker exec some-rabbit rabbitmqctl delete_queue print-requests
```

## Solución de Problemas

1. **Error de conexión a RabbitMQ:**
   - Verificar que el contenedor de RabbitMQ esté en ejecución.
   - Comprobar la configuración de host en `appsettings.json`.

2. **Errores de cola:**
   - Eliminar y recrear la cola si hay conflictos de configuración.
   - Verificar la configuración de MassTransit en `Program.cs`.

3. **Problemas de prioridad:**
   - Asegurarse de que la cola esté configurada con `x-max-priority`.
   - Verificar que las prioridades enviadas estén entre 1 y 10.

Para más ayuda, consultar los logs de la aplicación y de RabbitMQ.
