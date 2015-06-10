# SetAttrOfCSharpByGit
Автоматическое назначение версии по комитам гита.

###Для VisualStudio:
1.  Копируем SetAttrOfCSharpByGit.exe в каталог проекта.
2.  Добавляем новый файл AssemblyInfo.git.cs, действие при сборке: Нет.
3.  Заменяем в файле:

        [assembly: AssemblyVersion("0.0.0.0")]
        [assembly: AssemblyFileVersion("0.0.0.0")]

На

        [assembly: AssemblyVersion("{GitTagVersion}")]
        [assembly: AssemblyFileVersion("{GitTagVersion}")]

4.  Добавляем событие перед сборкой:

        "$(SolutionDir)SetAttrOfCSharpByGit.exe" "$(SolutionDir)\" "$(ProjectDir)Properties\AssemblyInfo.cs" "$(ProjectDir)Properties\AssemblyInfo.git.cs"

5.  Добавляем событие после сборки:

        "$(SolutionDir)SetAttrOfCSharpByGit.exe" "$(ProjectDir)Properties\AssemblyInfo.cs"
        
6.  Выполнять событие после собрки: всегда
