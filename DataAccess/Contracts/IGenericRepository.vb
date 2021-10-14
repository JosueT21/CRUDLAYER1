Public Interface IGenericRepository(Of Entity As Class)
    Function GetAll() As IEnumerable(Of Entity) REM  funcion para obtener todos los datos db
    Function Add(entity As Entity) As Integer REM funcion para agregar en la tabla de la db
    Function Edit(entity As Entity) As Integer REM funcion para editar en la base de datos
    Function Remove(id As Integer) As Integer REM funcion para eliminar registros en la base de datos

End Interface
