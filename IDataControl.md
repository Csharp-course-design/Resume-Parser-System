# 数据库操作接口说明

[TOC]

## 插入

以下是使用泛型版本的 `IDataInsert` 接口文档说明。泛型增强了接口的类型安全性和灵活性，便于适应不同类型的插入需求。

------

### **接口说明**

```csharp
namespace DAL.DataControl.Interface
{
    /// <summary>
    /// 数据插入接口，定义用于向数据库插入数据的标准操作方法，支持泛型类型。
    /// </summary>
    public interface IDataInsert
    {
        // 接口成员见以下详细说明
    }
}
```

------

### **方法说明**

#### **1. `Insert<T>`**

```csharp
void Insert<T>(T Item);
```

- **描述**：将指定类型的对象插入到数据库表中。

- 参数：

  - `Item`：`T`

     类型，表示待插入的表中数据对象。

    - 类型参数 `T` 可用作数据表的映射类型。

- **返回值**：无。

- **使用场景**：适用于仅需插入数据但不关心返回主键 ID 的场景。

------

#### **2. `InsertReturnID<T>`**

```csharp
string InsertReturnID<T>(T Item);
```

- **描述**：将指定类型的对象插入到数据库表中，并返回插入数据的唯一标识符（主键 ID）。

- 参数：

  - `Item`：`T` 类型，表示待插入的表中数据对象。

- **返回值**：字符串类型，表示插入数据生成的唯一标识符（例如主键 ID）。

- **使用场景**：适用于需要同时插入数据和获取插入记录 ID 的场景，例如当后续操作依赖于插入记录的主键时。

------

### **泛型的优势**

1. 类型安全性：

   - 编译时即可检查插入数据类型是否正确，减少运行时错误。

2. 代码重用性：

   - 支持多种数据类型的插入操作，无需为每个类型单独实现接口。

3. 灵活性：

   - 适应复杂的业务需求，例如插入不同类型的实体对象。

------

### **使用示例**

#### **插入记录**

假设需要插入一个 `User` 类型的对象到数据库表中：

```csharp
public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
}

// 实例化接口实现类
IDataInsert dataInsert = new YourDataInsertImplementation();
var newUser = new User { Name = "John Doe", Age = 30 };

dataInsert.Insert(newUser);
Console.WriteLine("用户已成功插入！");
```

------

#### **插入记录并获取主键 ID**

假设需要插入一个 `Product` 类型的对象到数据库表中，并获取生成的主键 ID：

```csharp
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// 实例化接口实现类
IDataInsert dataInsert = new YourDataInsertImplementation();
var newProduct = new Product { Name = "Laptop", Price = 999.99M };

string newID = dataInsert.InsertReturnID(newProduct);
Console.WriteLine($"新插入产品的ID是: {newID}");
```

------

### **实现建议**

#### **参数映射**

通过反射或映射工具（如 Dapper 或 EF）将泛型对象的属性映射为数据库字段。

```csharp
public class DataInsertImplementation : IDataInsert
{
    public void Insert<T>(T Item)
    {
        // 使用反射或 ORM 映射对象属性到 SQL 字段
        var sql = GenerateInsertSql(Item);
        ExecuteNonQuery(sql);
    }

    public string InsertReturnID<T>(T Item)
    {
        // 同样映射属性并执行插入
        var sql = GenerateInsertSql(Item) + "; SELECT SCOPE_IDENTITY();";
        return ExecuteScalar(sql).ToString();
    }

    private string GenerateInsertSql<T>(T Item)
    {
        // 示例：动态生成插入 SQL
        var type = typeof(T);
        var properties = type.GetProperties();
        var tableName = type.Name; // 假设表名与类名相同

        var columns = string.Join(", ", properties.Select(p => p.Name));
        var values = string.Join(", ", properties.Select(p => $"'{p.GetValue(Item)}'"));

        return $"INSERT INTO {tableName} ({columns}) VALUES ({values});";
    }
}
```

------

### **示例 SQL 实现**

#### **插入数据**

```sql
INSERT INTO Users (Name, Age) VALUES (@Name, @Age);
```

#### **插入数据并返回主键 ID**

```sql
INSERT INTO Users (Name, Age) VALUES (@Name, @Age);
SELECT SCOPE_IDENTITY(); -- 返回自增主键
```

通过 `IDataInsert` 接口的泛型版本，可以提升插入操作的灵活性、类型安全性和代码复用性，适合多种场景的数据访问层设计。

## 删除

以下是 `IDataDelete` 接口的文档说明。该接口定义了用于删除数据的基本操作方法，可以应用于实现数据删除功能的各种类中。

------

### **接口说明**

```csharp
namespace DAL.DataControl.Interface
{
    /// <summary>
    /// 数据删除接口，定义用于删除数据的标准操作方法。
    /// </summary>
    public interface IDataDelete
    {
        // 接口成员见以下详细说明
    }
}
```

------

### **方法说明**

#### **1. `DeleteByID`**

```csharp
int DeleteByID(string ID);
```

- **描述**：根据指定的唯一标识符（ID），删除数据库中对应的数据行。
- 参数：
  - `ID`：字符串类型，表示要删除的数据行的唯一标识。
- **返回值**：整数，表示受影响的行数。
- **使用场景**：适用于根据主键或唯一标识符删除单条记录的场景。

------

#### **2. `Delete`**

```csharp
int Delete(Dictionary<string, List<string>> Wheres);
```

- **描述**：根据指定的条件集合删除符合条件的数据行。

- 参数：

  - `Wheres`:`Dictionary<string, List<string>>`

     类型，键表示字段名称，值是字段可能的匹配值列表。

    - 若值列表包含多个值，则会生成类似 `field IN (value1, value2, ...)` 的条件。
    - 若键值为空列表，则忽略该字段的限制条件。

- **返回值**：整数，表示受影响的行数。

- **使用场景**：适用于根据复杂条件批量删除记录的场景。

------

### **使用示例**

#### **删除单条记录**

假设需要删除 ID 为 `12345` 的记录：

```csharp
IDataDelete dataDelete = new YourDataDeleteImplementation();
int rowsAffected = dataDelete.DeleteByID("12345");
Console.WriteLine($"受影响的行数: {rowsAffected}");
```

------

#### **按条件删除多条记录**

假设需要删除以下符合条件的记录：

- `Name` 字段为 `Alice` 或 `Bob`。
- `Age` 字段为 `30`。

示例代码：

```csharp
var conditions = new Dictionary<string, List<string>>
{
    { "Name", new List<string> { "Alice", "Bob" } },
    { "Age", new List<string> { "30" } }
};

IDataDelete dataDelete = new YourDataDeleteImplementation();
int rowsAffected = dataDelete.Delete(conditions);
Console.WriteLine($"受影响的行数: {rowsAffected}");
```

条件生成的 SQL 可能类似：

```sql
DELETE FROM TableName WHERE Name IN ('Alice', 'Bob') AND Age IN ('30');
```

------

## 更新

以下是 `IDataUpdate` 接口文档说明的详细内容：

------

### **接口概述**

`IDataUpdate` 接口定义了标准化的更新方法，用于将数据表中的记录根据条件或主键进行更新。接口提供了灵活的更新机制，支持单条记录和多条记录的批量更新。

------

### **方法说明**

#### **1. `Update`**

```csharp
int Update<T>(string ID, T Item);
```

- **描述**：根据指定的主键 `ID`，将对应记录的字段替换为 `Item` 对象中存放的数据。

- 参数：

  - `ID`：`string` 类型，指定数据记录的唯一标识。
  - `Item`：泛型 `T`，表示包含更新内容的对象。
  
- 返回值：

  - `int` 类型，表示受影响的行数。

- 适用场景：

  - 需要更新某条记录时，使用主键 `ID` 确定目标记录。

- 注意事项：

  - `Item` 中的字段需要与数据库表结构匹配。
- 数据库中必须存在主键为 `ID` 的记录，否则无数据被更新。

------

#### **2. `Update`（重载）**

```csharp
int Update<T>(T Item, Dictionary<string, List<string>> Wheres);
```

- **描述**：根据 `Wheres` 指定的条件，将所有符合条件的记录更新为 `Item` 对象中存放的数据。

- 参数：

  - `Item`：泛型 `T`，表示包含更新内容的对象。

  - `Wheres`：

    ```
    Dictionary<string, List<string>>
    ```
  
     类型，表示查询条件。

    - 键：字段名称。
    - 值：该字段可能的匹配值列表。
  
- 返回值：

  - `int` 类型，表示受影响的行数。

- 适用场景：

  - 批量更新符合某些条件的数据。

- 注意事项：

  - 如果 `Wheres` 为空或条件不明确，可能导致更新所有记录，应小心使用。
  - 使用 `WHERE` 子句保护数据安全。

------

### **接口使用示例**

#### **1. 更新指定 ID 的记录**

假设我们有一个 `User` 类：

```csharp
public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
}
```

调用 `Update` 方法更新指定 ID 的记录：

```csharp
IDataUpdate dataUpdate = new YourDataUpdateImplementation();

var userToUpdate = new User
{
    Name = "John Doe",
    Email = "john.doe@example.com"
};

int affectedRows = dataUpdate.Update("12345", userToUpdate);

Console.WriteLine($"成功更新的行数：{affectedRows}");
```

------

#### **2. 批量更新符合条件的记录**

```csharp
var updatedUser = new User
{
    Name = "Jane Doe",
    Email = "jane.doe@example.com"
};

var whereConditions = new Dictionary<string, List<string>>
{
    { "Status", new List<string> { "Inactive", "Pending" } },
    { "Role", new List<string> { "User" } }
};

int affectedRows = dataUpdate.Update(updatedUser, whereConditions);

Console.WriteLine($"成功更新的行数：{affectedRows}");
```

------

### **设计注意事项**

1. **动态构建 SQL 语句**：
   - `Item` 的属性和 `Wheres` 中的条件需要动态构建 `SET` 和 `WHERE` 子句。
   - 使用参数化查询避免 SQL 注入。
2. **泛型支持**：
   - `Item` 的类型应与目标数据库表的结构相符。
   - 可结合反射自动解析 `Item` 的属性名称和值。
3. **事务支持**：
   - 在批量更新场景下，建议使用事务处理，保证数据一致性。

------

### **示例 SQL 语句**

#### **1. 更新指定 ID 的记录**

假设表名为 `Users`，更新 ID 为 `12345` 的记录：

```sql
UPDATE Users
SET Name = 'John Doe', Email = 'john.doe@example.com'
WHERE ID = '12345';
```

#### **2. 批量更新符合条件的记录**

更新 `Status` 为 `Inactive` 或 `Pending` 且 `Role` 为 `User` 的所有记录：

```sql
UPDATE Users
SET Name = 'Jane Doe', Email = 'jane.doe@example.com'
WHERE Status IN ('Inactive', 'Pending') AND Role = 'User';
```

------

### **扩展功能建议**

1. **字段更新范围控制**：
   - 提供支持仅更新部分字段的功能。
   - 通过动态字段列表指定需要更新的列。
2. **条件校验**：
   - 在执行更新前，检查 `Wheres` 条件是否有效。
   - 如果条件为空，阻止执行更新。
3. **日志记录**：
   - 记录更新操作的日志，尤其是批量更新，便于追踪和排查问题。

通过 `IDataUpdate` 接口，可以高效、安全地实现数据库更新操作，同时满足单记录和批量更新需求。

## 查询

以下是 `IDataSelect` 接口文档说明的详细内容：

------

### **接口概述**

`IDataSelect` 接口定义了一组标准方法，用于根据条件从数据库中查询数据，并提供灵活的返回数据方式。接口的主要目标是提高查询的通用性和灵活性，支持多种查询需求。

------

### **方法说明**

#### **1. `Select`**

```csharp
DataSet Select(Dictionary<string, List<string>> Wheres);
```

- **描述**：通过指定的条件查询数据库，并以 `DataSet` 格式返回所有匹配的数据。

- 参数：

  - ` Wheres`：`Dictionary<string, List<string>>`

     类型，表示查询条件。

    - 键：数据库字段名称。
    - 值：对应字段可能的匹配值列表（支持多值匹配，类似于 SQL 中的 `IN` 语句）。

- 返回值：

  - 查询结果，返回 `DataSet` 类型的数据表集合。

- 适用场景：

  - 需要从数据库中提取全部列数据，按条件返回结果。

------

#### **2. `Select`（重载）**

```csharp
DataSet Select(List<string> Fields, Dictionary<string, List<string>> Wheres);
```

- **描述**：通过指定字段和条件查询数据库，并以 `DataSet` 格式返回结果。

- 参数：

  - `Fields`：`List<string>` 类型，表示需要返回的字段名称列表。
  - `Wheres`：`Dictionary<string, List<string>>` 类型，表示查询条件。

- 返回值：

  - 查询结果，返回 `DataSet` 类型的数据表集合。

- 适用场景：

  - 仅需要数据库中部分列数据时，避免查询所有列造成性能浪费。

------

#### **3. `SelectReturnObject`**

```csharp
T SelectReturnObject<T>(Dictionary<string, List<string>> Wheres);
```

- **描述**：通过指定条件查询数据库，并将查询结果直接映射为指定类型的对象。

- 参数：

  - `Wheres`：`Dictionary<string, List<string>>` 类型，表示查询条件。

- 返回值：

  - 泛型 `T` 类型对象，表示查询结果的数据类实例。

- 适用场景：

  - 需要将查询结果直接转换为业务对象，便于后续逻辑处理。

- 注意事项：

  - 数据库查询结果应与泛型类型 `T` 的属性对应。
  - 若查询结果为空，可能需要返回默认值或抛出异常。

------

### **使用示例**

#### **1. 使用条件查询所有数据**

```csharp
IDataSelect dataSelect = new YourDataSelectImplementation();

var whereConditions = new Dictionary<string, List<string>>
{
    { "Status", new List<string> { "Active", "Pending" } },
    { "Type", new List<string> { "Admin" } }
};

DataSet result = dataSelect.Select(whereConditions);

Console.WriteLine("查询到的数据表数量：" + result.Tables.Count);
```

------

#### **2. 使用字段筛选条件查询**

```csharp
var fields = new List<string> { "ID", "Name", "Email" };
var whereConditions = new Dictionary<string, List<string>>
{
    { "Status", new List<string> { "Active" } }
};

DataSet result = dataSelect.Select(fields, whereConditions);

foreach (DataTable table in result.Tables)
{
    foreach (DataRow row in table.Rows)
    {
        Console.WriteLine($"ID: {row["ID"]}, Name: {row["Name"]}, Email: {row["Email"]}");
    }
}
```

------

#### **3. 将查询结果映射为对象**

假设有一个 `User` 数据类：

```csharp
public class User
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
```

使用 `SelectReturnObject` 方法：

```csharp
var whereConditions = new Dictionary<string, List<string>>
{
    { "ID", new List<string> { "1" } }
};

User user = dataSelect.SelectReturnObject<User>(whereConditions);

if (user != null)
{
    Console.WriteLine($"用户信息：ID = {user.ID}, Name = {user.Name}, Email = {user.Email}");
}
else
{
    Console.WriteLine("未找到匹配的用户！");
}
```

------

### **设计建议**

#### **接口实现核心逻辑**

1. 条件转换：

   - 将 `Wheres` 参数解析为 SQL 的 `WHERE` 子句。
   - 支持多值匹配（`IN` 操作）和精确匹配。

2. 字段筛选：

   - `Fields` 参数用于动态构建 `SELECT` 子句。
   - 若未指定字段，默认查询所有字段（`SELECT *`）。

3. 数据映射：

   - `SelectReturnObject` 需要使用反射或 ORM 工具（如 Dapper 或 Entity Framework）将查询结果映射为业务对象。

------

### **示例 SQL 语句**

#### **1. 查询所有字段**

```sql
SELECT * FROM Users WHERE Status IN ('Active', 'Pending') AND Type = 'Admin';
```

#### **2. 查询指定字段**

```sql
SELECT ID, Name, Email FROM Users WHERE Status = 'Active';
```

------

通过 `IDataSelect` 接口，可以实现灵活的查询操作，满足多种业务场景的数据访问需求。尤其是 `SelectReturnObject` 提供了对象化返回方式，更贴近实际开发中的需求。
