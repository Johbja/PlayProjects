# C# Beginner Course — 10 Assignments

Welcome! This course teaches you the basics of C# through 10 hands-on assignments
that increase in difficulty. You write code, run the project, and get instant feedback.

---

## How to use this course

### 1. Open your assignment
All assignments live in the **`Assignments/`** folder.
Each file is named by number and topic so you always know where you are:

```
Assignments/
  01_VariablesAndTypes.cs
  02_StringInterpolation.cs
  03_IfElseConditionals.cs
  04_ForLoop.cs
  05_WhileLoop.cs
  06_Arrays.cs
  07_MethodsAndRecursion.cs
  08_ClassesAndObjects.cs
  09_ListsAndLinq.cs
  10_InterfacesAndPolymorphism.cs
```

> **These are the only files you need to touch.**

### 2. Read the instructions in the file
Every assignment file starts with a comment block explaining exactly what you need to implement.
Below the comments you will find a class with empty methods and `// TODO:` markers — that is
where you write your code.

Example (`04_ForLoop.cs`):
```csharp
public class ForLoop
{
    public int Sum(int n)
    {
        // TODO: sum integers 1..n using a for loop
        return 0;
    }
    ...
}
```

### 3. Run the project and check your result
Press **F5** in Visual Studio (or `dotnet run` in the terminal) to launch the course menu.

```
????????????????????????????????????????????????
?        C# Beginner Course — 10 Assignments   ?
????????????????????????????????????????????????

  Select an assignment to run its tests:

   [ 1]  Variables & Types
   [ 2]  String Interpolation
   ...
   [A]  Run ALL assignments
   [Q]  Quit

  >
```

- Type a **number** (e.g. `4`) and press Enter to test a single assignment.
- Type **A** to run all assignments and see an overview.
- Type **Q** to quit.

### 4. Read the feedback and fix your code
Each test tells you exactly what passed and what went wrong:

```
  ?  Sum(1) == 1
  ?  Sum(5) == 15
  ?  BuildCountdown(3) == "3, 2, 1"
       ? Expected "3, 2, 1", got ""
```

Go back to the file, fix the issue, and run again. Repeat until all tests are green. ??

---

## Assignments overview

| #  | File                              | Topic                     |
|----|-----------------------------------|---------------------------|
|  1 | `01_VariablesAndTypes.cs`         | Variables & types         |
|  2 | `02_StringInterpolation.cs`       | String interpolation      |
|  3 | `03_IfElseConditionals.cs`        | If / else conditionals    |
|  4 | `04_ForLoop.cs`                   | For loop                  |
|  5 | `05_WhileLoop.cs`                 | While loop                |
|  6 | `06_Arrays.cs`                    | Arrays                    |
|  7 | `07_MethodsAndRecursion.cs`       | Methods & recursion       |
|  8 | `08_ClassesAndObjects.cs`         | Classes & objects         |
|  9 | `09_ListsAndLinq.cs`              | Lists & LINQ              |
| 10 | `10_InterfacesAndPolymorphism.cs` | Interfaces & polymorphism |

---

## Folder structure explained

```
CSharpCourse/
  Assignments/      ? ??  YOUR WORKSPACE — open and edit files here
  _Framework/       ? ??  Engine that runs and grades your code
  README.md         ? ??  You are here
```

The `_Framework` contain the engine that runs and grades your code.
You do not need to open them. Modifying them may break the tests.

---

## Tips for beginners

- **Read the whole comment block** at the top of the assignment before writing any code.
- **Run often** — there is no penalty for running with wrong answers. Every run gives you feedback.
- **Fix one failing test at a time** — start from the top of the list.
- **The `// TODO:` comments** show you exactly where to write code. You normally only
  need to replace the `return 0;` / `return "";` placeholder.
- If you are stuck, re-read the description in the comment — it always contains an example.
