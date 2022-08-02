import re

def read_file():
    # file opening
    while True:
        path = input("Please enter a path to INI file:  ")
        if re.match(r"^.*\.INI$", path) is None:
            print("File is not .INI, try better next time")
            continue
        try:
            ini = open(path, 'r')
            break
        except FileNotFoundError:
            print("File not found, try again")
            continue
    temp = {}
    sec = re.compile(r"\[[\w\_]+\]")
    pair = re.compile(r"[\w\_]+\s=\s[\S]+[\n\s\w]+")
    sc = None
    for f in ini:
        if not (sec.match(f) is None):
            sc = sec.match(f).group(0)
            sc = sc[1:len(sc) - 1:1]
            temp[sc] = {}
        elif not (pair.match(f) is None):
            assert not(sc is None), "File have wrong format"
            key, t, val = f.split()[0:3]
            temp[sc][key] = val
        else:
            continue
    assert len(temp) > 0, ".INI file is empty, filepath = \""+path+"\""
    ini.close()
    return temp

def find_item(d = {}, _type = "string", _sec = "", _par = ""):
    assert _type == "int" or _type == "float" or _type == "string", "Wrong type"
    assert _sec in d, "Section '" + _sec + "' not found"
    assert _sec in d and _par in d[_sec], "Parameter '" + _par + "' not exist in '" + _sec + "' section"
    t = 0
    if _type == "int":
        try:
            t = int(d[_sec][_par])
        except TypeError:
            "Wrong type of parameter"
    elif _type == "float":
        try:
            t = float(d[_sec][_par])
        except TypeError:
            "Wrong type of parameter"
    else:
        t = int(d[_sec][_par])
    print(t)

if __name__ == "__main__":
    d = read_file()
    print("If You want to ask about something:\n" \
            "<type> <Section> <Parameter>\n" \
            "Example:\n" \
            "int COMMON LogNCMD\n" \
            "<type> is 'int', 'float' or 'string'\n" \
            "If You want to end working with INI, enter -1")
    while True:
            inp = input()
            if inp == "-1":
                break
            if len(inp.split()) == 3:
                _type, _sec, _par = inp.split()
            else:
                print("Wrong amount of arguments or something another goes wrong")
                continue
            find_item(d, _type, _sec, _par)
