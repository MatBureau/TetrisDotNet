   M S B A M L   `   `   `  ????G  CTetrisDotNet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TetrisDotNetTetrisDotNet  5   	       TetrisDotNet.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:TetrisDotNet     5   	   -   5          ? Title$  Tetris??5   	   $	??600??6   $	??800??6%   $	??600??61   $	??600??6@   
???????$??Segoe UI Light)?5	   	   $??28(?6%    ? 
ResizeMode$ NoResize=?6L    ? WindowStyle$
 None=?6b   .??5
      ?  ? RowDefinitions 5   
   ?? 5      $
??auto ?6   ?? 5      $??* ?6    ? ColumnDefinitions 5   
   ?? 5      $??* ?6   6+   ?? 5      $
??auto ?6   6.   ?? 5      $??* ?6   6+   ??5   
   ?? 5       ?? ImageSource$ Assets/Background.png??6   .+?5   
   (? $??1??6   $??1??5      $??0,0,0,20q?5      .??5      ?? -   5         ??Name$ 
GameCanvas??5      
???   ?$	??250??5      $	??510??5      $
}?True??5      5      5       ?? 5!   
   -   5         ??Name$ 	TXB_Score??5!      $??0??5"      $??1??5#      $??Score : ??5$      $
??0,10q?5%       ?? TextAlignment$ Center=?5&      ?? 5'   
   $??1??6   $??0??5(      $??Center=?5)      $??Right=?5*      .+?5+      ?? $??Réserve??6   $ Center=?5,      ?? 5-      -   5        	 ??Name$	 	HoldImage??5-      $??20q?5.      $	??125??5/      50      ?? 51   
   $??1??6   $??2??52      $??Center=?53      $
??Left=?54      .+?55      ?? $??   Pièce Suivante??6   $ Center=?56      ?? 57      -   5        $	 	NextImage??57      $??40,20,20,20q?58      $	??125??59      5:      ? 5;   
   -   5        
 ?Name$
 GameOverPanel??5;      ???	#CC000000$??2??5=      $??3??5>      $y?Hidden=?6#   .+?5?      ?? $??Center=?6   $??Center=?66   .+?5@      ?? -   5        $ TBX_GameOver??5@      $??	Game Over??62   $??48(?6C   $ Center=?6Q   6j   ?? 5A      -   5        $ TBX_ScoreFinal??5A      $??Score final :??64   $??25(?6I   $ Center=?6W   6p   ?? 5B      -	   5        $??	Rejouer !??5B      
???????$??0,20,0,0q?6E   $??5q?6W   6}   ?? 5C      -
   5         ??Name$ BTN_Continue??5C      $??	Continuer??6/   
???????$??0,20,0,0q?6[   $??5q?6m   6y   5E      5F      5G      5H      