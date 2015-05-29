# SetAttrOfCSharpByGit
Автоматическое назначение версии по комитам гита

1.  Копируем SetAttrOfCSharpByGit.exe в каталог проэкта
2.  Добавляем событие перед сборкой:

        "$(SolutionDir)SetAttrOfCSharpByGit.exe" "start" "$(SolutionDir)\" "$(ProjectDir)Properties\AssemblyInfo.cs"
2.  Добавляем событие после сборки:

        "$(SolutionDir)SetAttrOfCSharpByGit.exe" "end" "$(ProjectDir)Properties\AssemblyInfo.cs"
