# Task 2

It's necessary skills for every programmer — process files, calculate checksums, follow the instruction, etc.

Language: either C# or JavaScript or TypeScript or Ruby or PHP or Python or Java or anything you like (**you don't submit the code, only the result**).

1. Calculate **SHA3-256** for every file from archive (https://www.dropbox.com/s/oy2668zp1lsuseh/task2.zip?dl=1). Note, **files are binary, you don’t need encodings **— if you read file to string with some encoding, you have to use the same encoding to decode string into bytes back for hashing (there is a technical term for such conversions — “stupid activity”).

2. Write hashes as **64 hex digits in lower case.**

3. Sort (ascending) hashes as strings (not chars in hashes, but hashes as whole).

4. Join sorted hashes **without any separator**.

5. Concatenate resulted string with your e-mail in lowercase.

6. Find the SHA3-256 of the result string.

Send obtained 64 hex digits in the lower case to the https://discord.com/channels/950701840901746708/1227938092728586320 channel using the following command
```
!task2 email 64hexdigitshere
```
Note: SHA3-256 is not the same algorithm as SHA-256. 

Some additional hints (based on the experience of previous groups): 
* check if you use SHA3-256, 
* check if you process exactly 256 required files (not everything in the some directory),
* check if you concatenate your strings without separator — beware of JavaScript's join!
* check if you write e-mail in lower case and e-mail goes to the end of the resulted string, 
* and, of course, you have to calculate separate hash for every file, not to update the same hash with every file.

> **Never work with *binary* files in text editors** — if your IDE, e.g., changes automagically even a single byte, your won't get a proper result (redownload the files if necessary).