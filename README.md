# SetAttrOfCSharpByGit
Автоматическое назначение версии по комитам гита.

###Для VisualStudio:
1.  Копируем SetAttrOfCSharpByGit.exe в каталог проекта.
2.  Добавляем событие перед сборкой:

        "$(SolutionDir)SetAttrOfCSharpByGit.exe" "start" "$(SolutionDir)\" "$(ProjectDir)Properties\AssemblyInfo.cs"
3.  Добавляем событие после сборки:

        "$(SolutionDir)SetAttrOfCSharpByGit.exe" "end" "$(ProjectDir)Properties\AssemblyInfo.cs"
4.  В файле "AssemblyInfo.cs" используем строку {GitCommitsCount}, для замены на номер.
