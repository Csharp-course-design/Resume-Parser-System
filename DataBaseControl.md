# `DataBaseControl` 类的文档

以下是 `DataBaseControl` 类的文档说明。该类旨在提供基础的数据访问功能，包括数据库连接管理、SQL执行以及数据检查等。

------

### **类说明**

```csharp
namespace DAL.DataControl
{
    /// <summary>
    /// 数据访问基类，用于提供数据访问的基础功能。
    /// 包括数据库连接管理、SQL执行、数据检查、以及SQL WHERE子句构建等实用方法。
    /// </summary>
    public abstract class DataBaseControl
    {
        // 类的成员和方法见以下详细说明
    }
}
```

------

### **字段说明**

#### **`sqlConnection`**

```csharp
private static SqlConnection sqlConnection;
```

- **描述**：数据库连接对象，采用单例模式。
- **作用**：用于管理与数据库的连接，避免重复创建连接实例。

------

### **方法说明**

#### **数据库连接管理**

1. **`GetSqlConnection`**

```csharp
public static SqlConnection GetSqlConnection()
```

- **描述**：创建并获取单例的数据库连接对象。
- **返回值**：`SqlConnection` 对象。
- **注意**：如果 `sqlConnection` 已经存在，则直接返回现有实例。

------

1. **`OpenSqlConnection`**

```csharp
public static void OpenSqlConnection()
```

- **描述**：打开数据库连接。
- **注意**：仅当连接状态不为 `Open` 时才执行。

------

1. **`CloseSqlConnection`**

```csharp
public static void CloseSqlConnection()
```

- **描述**：关闭数据库连接。
- **注意**：仅当连接状态不为 `Closed` 时才执行。

------

#### **SQL 执行**

1. **`ExecuteSQL`**

```csharp
public int ExecuteSQL(SqlTransaction sqlTransaction, string sql, List<SqlParameter> sqlParameters)
```

- **描述**：执行单条 SQL 语句。
- 参数：
  - `sqlTransaction`：事务对象（可选）。
  - `sql`：待执行的 SQL 语句。
  - `sqlParameters`：SQL 参数列表，用于防止 SQL 注入。
- **返回值**：受影响的行数。

------

1. **`ExecuteGroupSQL`**

```csharp
public bool ExecuteGroupSQL(List<string> SQLs)
```

- **描述**：通过事务执行一组 SQL 语句。
- 参数：
  - `SQLs`：待执行的 SQL 语句列表。
- **返回值**：`true` 表示执行成功，`false` 表示执行失败并回滚。

------

#### **数据检查**

1. **`CheckSex`**

```csharp
public static bool CheckSex(string Data)
```

- **描述**：检查性别字段是否合法。
- **参数**：`Data` - 待检查的数据。
- **返回值**：`true` 表示合法，`false` 表示不合法。

------

1. **`CheckDate`**

```csharp
public static bool CheckDate(string Data)
```

- **描述**：检查日期格式是否合法（格式：YYYY-MM-DD）。
- **参数**：`Data` - 待检查的数据。
- **返回值**：`true` 表示合法，`false` 表示不合法。

------

1. **`CheckEmall`**

```csharp
public static bool CheckEmall(string Data)
```

- **描述**：检查邮箱格式是否合法。
- **参数**：`Data` - 待检查的数据。
- **返回值**：`true` 表示合法，`false` 表示不合法。

------

1. **`CheckMobilePhone`**

```csharp
public static bool CheckMobilePhone(string Data)
```

- **描述**：检查手机号格式是否合法（格式：11 位数字）。
- **参数**：`Data` - 待检查的数据。
- **返回值**：`true` 表示合法，`false` 表示不合法。

------

#### **WHERE 子句构建**

1. **`BuildWhereClause (Dictionary<string, string>)`**

```csharp
public static string BuildWhereClause(Dictionary<string, string> conditions)
```

- **描述**：构建 SQL 的 WHERE 子句（支持单值匹配或模糊匹配）。
- 参数：
  - `conditions`：键为字段名，值为匹配值。
- **返回值**：构建的 WHERE 子句字符串。

------

1. **`BuildWhereClause (Dictionary<string, HashSet<string>>)`**

```csharp
public static string BuildWhereClause(Dictionary<string, HashSet<string>> conditions)
```

- **描述**：构建 SQL 的 WHERE 子句（支持多值匹配）。
- 参数：
  - `conditions`：键为字段名，值为可匹配的多个值。
- **返回值**：构建的 WHERE 子句字符串。

------

### **使用示例**

#### **执行单条 SQL**

```csharp
List<SqlParameter> parameters = new List<SqlParameter>
{
    new SqlParameter("@Name", "John Doe")
};

int rowsAffected = DataBaseControl.ExecuteSQL(null, "INSERT INTO Users (Name) VALUES (@Name)", parameters);
```

#### **构建 WHERE 子句**

```csharp
var conditions = new Dictionary<string, string>
{
    { "Name", "John%" },
    { "Age", "30" }
};

string whereClause = DataBaseControl.BuildWhereClause(conditions);
// 输出：Name LIKE 'John%' AND Age = '30'
```

