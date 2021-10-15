import importlib
import sys

importlib.reload(sys)

if __name__ == "__main__":

    # sys.setdefaultencoding("utf8")
    sr = open("SkillData.txt", "r", encoding="utf-8")
    sw = open(
        "D:\ProjectMercury\PorjectMercury\Assets\StreamingAssets\SkillData.json",
        "w",
        encoding="utf-8",
    )

    sw.write('{\n"SkillJsonClass":[\n\t\t')
    line = sr.readline()
    while True:
        line1 = line
        line = sr.readline()
        if not line1:
            break
        st = line1.split("\t")
        sw.write("{")
        sw.write('"SkillName":')
        sw.write('"' + st[0] + '"')
        sw.write(',"ClassName":')
        sw.write('"' + st[1] + '"')
        sw.write(',"SkillDetail":')
        sw.write('"' + st[2] + '"')
        sw.write(',"ReleaseTime":')
        sw.write(st[3])
        sw.write(',"CoolDown":')
        sw.write(st[4])
        sw.write(',"SkillState":')
        sw.write('"' + st[5] + '"')
        sw.write(',"SkillType":')
        sw.write('"' + st[6] + '"')
        sw.write(',"Data":[')

        leng = 6
        for i in range(7, len(st)):
            if st[i] == "":
                continue
            if st[i] == "\n":
                continue
            leng += 1
        for i in range(7, leng):
            sw.write(st[i] + ",")
        if leng != 6:
            sw.write(st[leng])
        sw.write("]}")
        if line:
            sw.write(",\n\t\t")
        else:
            sw.write("\n\t")

    sw.write("]\n}")
