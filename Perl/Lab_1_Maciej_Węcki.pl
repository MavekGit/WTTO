#Autor Maciej Wêcki

use strict;
use warnings;


truncate 'wynik.txt', 0;

# Szukamy plików o rozszerzeniu .txt
my $sum = 0;
my $i = 0;
my $firstColumn;
my $firstColumnAmino;
my $secondColumn;
my $fcAmino;
my $thirdColumn;
my $fcMerged = '';
my $scMerged = '';
my $thMerged = '';
my $allMerged = '';
my $name = '';

opendir(my $dh, 'Predictions') or die "Nie mo¿na otworzyæ bie¿¹cego katalogu: $!";
while (my $filename = readdir $dh) {
#next unless $filename =~ /1AC6inA.txt$/; # Ignorujemy pliki bez .txt
next unless $filename =~ /\.txt$/; # Ignorujemy pliki bez .txt
open(my $fh, '<', "Predictions/$filename") or die "Nie mo¿na otworzyæ pliku '$filename': $!";
while (my $line = <$fh>) {
# Odczytanie liczby jako tekstu i dodanie jej do sumy
#my ($number) = $line =~ /(\d+)/;
#$sum += $number if defined $number;

  if ($line =~ /^([A-Z]{3})\s/) {
    $firstColumn = $1;

    my @columns = split(/\s+/, $line);
    my $lastColumn = $columns[-1];

    if($lastColumn =~ /[A-Z]/ && scalar @columns > 3)   {

      #print "Last column not empty \n";

      $fcMerged = join(",", $fcMerged, $firstColumn);
      
      $secondColumn = $columns[2];
      $scMerged = join('',$scMerged, $secondColumn);
      
      $thirdColumn = $columns[-1];

      $thMerged = join('',$thMerged, $thirdColumn);
      
      }
    else{
    #print "Last column empty \n";
    }

  }


  $name = substr($filename, 0, 4);


}
close $fh;


open(my $fh2, '<', "Nazwy_aminokwasow.txt") or die "Nie mo¿na otworzyæ pliku 'Nazwy_aminokwasow.txt': $!";

while (my $line = <$fh2>) {

 if ($line =~ /^([A-Z]{3})\s/) {
    $fcAmino = $1;

    my @colAmino = split(/\s+/, $line);
    my $firstColumnAmino = $colAmino[1];

    $fcMerged =~ s/\Q$fcAmino/$firstColumnAmino/g;

      #print "$fcAmino \n";
    }
    #print $line;
}
close $fh2;

  $fcMerged =~ s/,//g;

   $allMerged = join("\n","> $name" , $fcMerged,$scMerged,$thMerged,"\n");

open(my $file, ">>" , "results.txt") or die "Can't open results.txt for appending: $!";

print $file $allMerged;

close $file;

$fcMerged = '';
$scMerged = '';
$thMerged = '';
$allMerged = '';
$name = '';


}

closedir $dh;


#print "$fcMerged\n";
#print "$scMerged\n";
#print "$thMerged\n";
print $allMerged;

