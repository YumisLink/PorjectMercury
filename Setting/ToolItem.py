import importlib
import sys

importlib.reload(sys)

if __name__ == "__main__":

    # sys.setdefaultencoding("utf8")
    sr = open("ItemData.txt", "r", encoding="utf-8")
    sw = open(
        "D:\ProjectMercury\PorjectMercury\Assets\StreamingAssets\ItemData.json",
        "w",
        encoding="utf-8",
    )

    sw.write('{\n"ItemJsonClass":[\n\t\t')
    line = sr.readline()
    while True:
        line1 = line
        line = sr.readline()
        if not line1:
            break
        st = line1.split("\t")
        sw.write("{")
        sw.write('"id":')
        sw.write(st[0])
        sw.write(',"ClassName":')
        sw.write('"' + st[1] + '"')
        sw.write(',"ItemName":')
        sw.write('"' + st[2] + '"')
        sw.write(',"ItemQuality":')
        sw.write('"' + st[3] + '"')
        sw.write(',"ItemDescribe":')
        sw.write('"' + st[4] + '"')
        sw.write(',"ItemType":')
        sw.write('"' + st[5] + '"')
        sw.write(',"Data":[')
        leng = 5
        for i in range(6, len(st)):
            if st[i] == "":
                continue
            if st[i] == "\n":
                continue
            leng += 1
        for i in range(6, leng):
            sw.write(st[i] + ",")
        if leng != 5:
            sw.write(st[leng])
        sw.write("]}")
        if line:
            sw.write(",\n\t\t")
        else:
            sw.write("\n\t")

    sw.write("]\n}")
