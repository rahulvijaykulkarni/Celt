function lockcontrol() {
    return !1
}

function ValidateNumberKeyPress(n, t, i) {
    var r = t.which ? t.which : event.keyCode,
        u = String.fromCharCode(r),
        f;
    if (i == !0) {
        if (r > 31 && (r < 48 || r > 57) && u != "." && u != "-") return !1
    } else if (r > 31 && (r < 48 || r > 57) && u != ".") return !1;
    return u == "." && n.value.indexOf(".") != -1 ? !1 : u == "-" &&
        (n.value.indexOf("-") != -1 || (f = getCaretPosition(n), f != 0)) ? !1 : !0
}

function ValidateNumberKeyUp(n) {
    var t, i, r;
    if (document.selection.type != "Text") {
        if (t = getCaretPosition(n), i = n.value.length, UnFormatNumber(n), r = /^-?\d+\.{0,1}\d*$/.test(n.value), !r) return setSelectionRange(n, t, t), !1;
        n.value = FormatNumber(n.value);
        i = n.value.length - i;
        setSelectionRange(n, t + i, t + i)
    }
}

function ValidateAndFormatNumber(n, t) {
    var i, r;
    if (n.value != "") {
        if (UnFormatNumber(n), i = /^-?\d+\.{0,1}\d*$/.test(n.value), !i) {
            alert("Not a number");
            n.value = "0.00";
            n.focus();
            n.select();
            return
        }
        if (t == "Percentage" && parseFloat(n.value) > 100) {
            alert("Percentage Input can not exceed 100.");
            n.value = "100.00";
            n.focus();
            n.select();
            return
        }
        isNaN(parseFloat(n.value)) && (alert("Number exceeding float range"), n = "0.00", n.focus(), n.select());
        r = parseFloat(n.value);
        n.value = r.toFixed(2);
        n.value = addthousandseprator(n.value)
    }
}

function ValidateAndFormatNumberControl(n) {
    var t = n.value;
    t = t.replace(/,/g, "");
    t += "";
    x = t.split(".");
    x1 = x[0];
    x2 = x.length > 1 ? "." + x[1] : "";
    for (var i = /(\d+)(\d{3})/, r = 0, f = String(x1).length, u = parseInt(f / 2 - 1); i.test(x1);)
        if (r > 0 ? x1 = x1.replace(i, "$1,$2") : (x1 = x1.replace(i, "$1,$2"), i = /(\d+)(\d{2})/), r++, u--, u == 0) break;
    n.value = x1 + x2
}

function FormatNumber(n) {
    var o = n,
        f = !1,
        t;
    n.charAt(0) == "-" && (f = !0, n = n.substr(1, n.length - 1));
    psplit = n.split(".");
    var i = psplit[0],
        r = [],
        e = i.length,
        s = Math.floor(e / 3),
        u = i.length % 3 || 3;
    for (t = 0; t < e; t += u) t != 0 && (u = 3), r[r.length] = i.substr(t, u), s -= 1;
    return n = r.join(","), o.indexOf(".") != -1 && (n += "." + psplit[1]), f == !0 && (n = "-" + n), n
}

function UnFormatNumber(n) {
    n.value != "" && (n.value = n.value.replace(/,/gi, ""))
}

function getCaretPosition(n) {
    var n = window.event.srcElement,
        t = n.value.length;
    if (n.createTextRange)
        for (objCaret = document.selection.createRange().duplicate(); objCaret.parentElement() == n && objCaret.move("character", 1) == 1;) --t;
    return t
}

function setSelectionRange(n, t, i) {
    if (n.setSelectionRange) n.focus(), n.setSelectionRange(t, i);
    else if (n.createTextRange) {
        var r = n.createTextRange();
        r.collapse(!0);
        r.moveEnd("character", i);
        r.moveStart("character", t);
        r.select()
    }
}

function chkIntInput(n, t) {
    try {
        if (n.readOnly == !0) return !0;
        var i, r;
        if (window.event) i = window.event.keyCode;
        else if (t) i = t.which;
        else return !0;
        return i == 46 ? !1 : i == null || i == 0 || i == 8 || i == 13 || i == 27 || i == 46 ? !0 : (r = String.fromCharCode(i), /\d/.test(r) ? (window.status = "", !0) : (window.status = "Field accepts numbers only.", !1))
    } catch (u) {
        alert("An error has occurred: " + u.message)
    }
}

function parseMonth(n) {
    var t = monthNames.filter(function(t) {
        return new RegExp("^" + n, "i").test(t)
    });
    if (t.length == 0) throw new Error("Invalid month string");
    if (t.length < 1) throw new Error("Ambiguous month");
    return monthNames.indexOf(t[0])
}

function DateInRange(n, t, i) {
    if (t < 0 || t > 11) throw new Error("Invalid month value.  Valid months values are 1 to 12");
    var r = 11 == t ? new Date(n + 1, 0, 0) : new Date(n, t + 1, 0);
    if (i < 1 || i > r.getDate()) throw new Error("Invalid date value.  Valid date values for " + monthNames[t] + " are 1 to " + r.getDate().toString());
    return !0
}

function CheckDate(n) {
    if (n.value.length > 0) try {
        var t = parseDateString(n.value);
        return t.getFullYear() < 1753 ? (alert("Please enter date greater then to year 1752"), n.value = "", !1) : (n.value = ("0" + t.getDate().toString()).right(2) + "/" + monthNames[t.getMonth()].left(3) + "/" + t.getFullYear().toString(), !0)
    } catch (i) {
        return alert(i.message), n.value = "", !1
    }
}

function parseDateString(n) {
    var r = n.replace(".", " ").replace(".", " ").replace("-", " ").replace("-", " ").replace("/", " ").replace("/", " ").replace("/", " "),
        u = n.replace(".", "").replace(".", "").replace(".", "").replace("-", "").replace("-", "").replace("-", "").replace("/", "").replace("/", "").replace("/", ""),
        t;
    if (!(n.length > u.length + 2))
        for (t = 0; t < dateParsePatterns.length; t++) {
            var f = dateParsePatterns[t].re,
                e = dateParsePatterns[t].handler,
                i = f.exec(r);
            if (i) return e(i)
        }
    throw new Error("Invalid date string");
}

function txtDate(n) {
    n.keyCode < 45 || n.keyCode > 122 ? n.keyCode = 0 : n.keyCode > 57 && n.keyCode < 97 && (n.keyCode < 65 || n.keyCode > 90) && (n.keyCode = 0)
}

function clearAll() {
    for (var t = document.getElementsByTagName("input"), n = 0; n < t.length; n++) t[n].type == "text" && (t[n].value = "")
}

function getNum(n) {
    try {
        var i = n.toString(),
            t = i.replace(/,/g, "");
        return n = t, isNaN(Number(n)) || n == "" ? 0 : Number(n)
    } catch (r) {
        alert("An error has occurred: Module-getNum:" + t + r.message)
    }
}

function addthousandseprator(n) {
    n += "";
    x = n.split(".");
    x1 = x[0];
    x2 = x.length > 1 ? "." + x[1] : "";
    for (var t = /(\d+)(\d{3})/, i = 0, u = String(x1).length, r = parseInt(u / 2 - 1); t.test(x1);)
        if (i > 0 ? x1 = x1.replace(t, "$1,$2") : (x1 = x1.replace(t, "$1,$2"), t = /(\d+)(\d{2})/), i++, r--, r == 0) break;
    return x1 + x2
}

function fillcategorycombojs(n) {
    try {
        n == "2010-11" || n == "2011-12" ? (catenew.style.display = "none", cateold.style.display = "") : (catenew.style.display = "", cateold.style.display = "none")
    } catch (t) {}
}

function setStatuscombojs(n, t, i, r, u) {
    n.value == "Individual" ? (t.style.display = "", i == !1 && (r.style.display = "", u.readOnly = !0)) : (t.style.display = "none", i == !1 && (r.style.display = "none", u.readOnly = !1))
}

function calculateDatajs(n, t, i, r, u, f, e, o, s, h, c, l, a, v, y, p, w, b, k, d, g, nt, tt, it, rt, ut, ft) {
    n == "2010-11" ? calculateData201011(n, t, i, r, u, f, e, o, s, h, a, v, y, p, w, b, k, d, g, nt, tt, it, rt, ut, ft) : n == "2011-12" ? calculateData201112(n, t, i, r, u, f, e, o, s, h, a, v, y, p, w, b, k, d, g, nt, tt, it, rt, ut, ft) : n == "2012-13" ? calculateData201213(n, t, i, r, u, f, e, o, s, h, a, v, y, p, w, b, k, d, g, nt, tt, it, rt, ut, ft) : n == "2013-14" ? calculateData201314(n, t, i, r, u, f, e, o, s, h, a, v, y, p, w, b, k, d, g, nt, tt, it, rt, ut, ft) : n == "2014-15" ? calculateData201415(n, t, i, r, u, f, e, o, s, h, a, v, y, p, w, b, k, d, g, nt, tt, it, rt, ut, ft) : n == "2015-16" ? calculateData201516(n, t, i, r, u, f, e, o, s, h, a, v, y, p, w, b, k, d, g, nt, tt, it, rt, ut, ft) : n == "2016-17" ? calculateData201617(n, t, i, r, u, f, e, o, s, h, a, v, y, p, w, b, k, d, g, nt, tt, it, rt, ut, ft) : n == "2017-18" && calculateData201718(n, t, i, r, u, f, e, o, s, h, c, l, a, v, y, p, w, b, k, d, g, nt, tt, it, rt, ut, ft, c, l)
}

function calculateData201718(n, t, i, r, u, f, e, o, s, h, c, l, a, v, y, p, w, b, k, d, g, nt, tt, it, rt, ut, ft) {
    var et = 0,
        st = 0,
        ht = 0,
        ct = 0,
        ot = parseInt(getNum(r.value)),
        wt = 0,
        bt = 0,
        at, vt, kt, p;
    if (t == "Domestic Company") at = 0, vt = 0, l.checked == !0 ? (et = ot * .29, at = 29e5, vt = 29e6) : c.checked == !0 ? (et = ot * .25, at = 25e5, vt = 25e6) : (et = ot * .3, at = 3e6, vt = 3e7), et = Math.round(et, 0), ot > 1e7 && ot <= 1e8 ? (adjustablesurcharge = et * .07, et + adjustablesurcharge > at + (ot - 1e7) ? (marginalrelief = et + adjustablesurcharge - (at + (ot - 1e7)), st = adjustablesurcharge - marginalrelief) : st = adjustablesurcharge, st = Math.round(st, 0)) : ot > 1e8 && (adjustablesurcharge = et * .12, et + adjustablesurcharge > vt * 1.07 + (ot - 1e8) ? (marginalrelief = et + adjustablesurcharge - (vt * 1.07 + (ot - 1e8)), st = adjustablesurcharge - marginalrelief) : st = adjustablesurcharge, st = Math.round(st, 0)), ht = (et + st) * .02, ct = (et + st) * .01;
    else if (t == "Foreign Company") et = ot * .4, et = Math.round(et, 0), ot > 1e7 && ot <= 1e8 ? (adjustablesurcharge = et * .02, et + adjustablesurcharge > ot - 6e6 ? (marginalrelief = et + adjustablesurcharge - (ot - 6e6), st = adjustablesurcharge - marginalrelief) : st = adjustablesurcharge, st = Math.round(st, 0)) : ot > 1e8 && (adjustablesurcharge = et * .05, et + adjustablesurcharge > ot - 592e5 ? (marginalrelief = et + adjustablesurcharge - (ot - 592e5), st = adjustablesurcharge - marginalrelief) : st = adjustablesurcharge, st = Math.round(st, 0)), ht = (et + st) * .02, ct = (et + st) * .01;
    else if (t == "LLP") et = ot * .3, et = Math.round(et, 0), ot >= 1e7 && (adjustablesurcharge = et * .12, et + adjustablesurcharge > ot - 7e6 ? (marginalrelief = et + adjustablesurcharge - (ot - 7e6), st = adjustablesurcharge - marginalrelief) : st = adjustablesurcharge, st = Math.round(st, 0)), ht = (et + st) * .02, ct = (et + st) * .01;
    else if (t == "Firms") et = ot * .3, et = Math.round(et, 0), ot >= 1e7 && (adjustablesurcharge = et * .12, et + adjustablesurcharge > ot - 7e6 ? (marginalrelief = et + adjustablesurcharge - (ot - 7e6), st = adjustablesurcharge - marginalrelief) : st = adjustablesurcharge, st = Math.round(st, 0)), ht = (et + st) * .02, ct = (et + st) * .01;
    else if (t == "Co-operative Society") ot <= 1e4 ? et = ot * .1 : ot > 1e4 && ot <= 2e4 ? et = (ot - 1e4) * .2 + 1e3 : ot > 2e4 && (et = (ot - 2e4) * .3 + 3e3), et = Math.round(et, 0), ot >= 1e7 && (adjustablesurcharge = et * .12, et + adjustablesurcharge > ot - 7003e3 ? (marginalrelief = et + adjustablesurcharge - (ot - 7003e3), st = adjustablesurcharge - marginalrelief) : st = adjustablesurcharge, st = Math.round(st, 0)), ht = (et + st) * .02, ct = (et + st) * .01;
    else if (t == "AOPs/BOI") ot <= 25e4 ? et = 0 : ot > 25e4 && ot <= 5e5 ? et = (ot - 25e4) * .1 : ot > 5e5 && ot <= 1e6 ? et = (ot - 5e5) * .2 + 25e3 : ot > 1e6 && (et = (ot - 1e6) * .3 + 125e3), et = Math.round(et, 0), ot >= 1e7 && (adjustablesurcharge = et * .15, et + adjustablesurcharge > ot - 7175e3 ? (marginalrelief = et + adjustablesurcharge - (ot - 7175e3), st = adjustablesurcharge - marginalrelief) : st = adjustablesurcharge, st = Math.round(st, 0)), ht = (et + st) * .02, ct = (et + st) * .01;
    else if (t == "HUF") ot <= 25e4 ? et = 0 : ot > 25e4 && ot <= 5e5 ? et = (ot - 25e4) * .1 : ot > 5e5 && ot <= 1e6 ? et = (ot - 5e5) * .2 + 25e3 : ot > 1e6 && (et = (ot - 1e6) * .3 + 125e3), et = Math.round(et, 0), ot >= 1e7 && (adjustablesurcharge = et * .15, et + adjustablesurcharge > ot - 7175e3 ? (marginalrelief = et + adjustablesurcharge - (ot - 7175e3), st = adjustablesurcharge - marginalrelief) : st = adjustablesurcharge, st = Math.round(st, 0)), ht = (et + st) * .02, ct = (et + st) * .01;
    else if (t == "Individual") {
        if (ft == !1) {
            var lt = parseInt(getNum(a.value)),
                yt = 0,
                pt = parseInt(getNum(y.value));
            yt = lt;
            lt > v && pt > 5e3 && (lt = lt + pt);
            lt = lt - (Math.min(parseInt(getNum(w.value)), p) + parseInt(getNum(b.value)) + parseInt(getNum(k.value)) + parseInt(getNum(d.value)));
            yt = yt - (Math.min(parseInt(getNum(w.value)), p) + parseInt(getNum(b.value)) + parseInt(getNum(k.value)) + parseInt(getNum(d.value)));
            wt = Math.min(parseInt(getNum(w.value)), p) + parseInt(getNum(b.value)) + parseInt(getNum(k.value)) + parseInt(getNum(d.value));
            lt < 0 && (lt = 0);
            yt < 0 && (yt = 0);
            g.value = addthousandseprator(yt);
            et = lt > v && pt > 5e3 ? calcindividualTax(lt, n, u, i) - calcindividualTax(v + pt, n, u, i) : calcindividualTax(lt, n, u, i);
            et < 0 && (et = 0);
            et = Math.round(et, 0);
            nt.value = addthousandseprator(et);
            et = et + parseInt(getNum(tt.value)) + parseInt(getNum(it.value)) + parseInt(getNum(rt.value)) + parseInt(getNum(ut.value));
            bt = parseInt(getNum(tt.value)) + parseInt(getNum(it.value)) + parseInt(getNum(rt.value)) + parseInt(getNum(ut.value));
            u != "Non-Resident" && ot <= 5e5 && (et = et - 5e3, et < 0 && (et = 0))
        } else wt = 0, bt = 0, et = calcindividualTax(ot, n, u, i), u != "Non-Resident" && ot <= 5e5 && (et = et - 5e3, et < 0 && (et = 0));
        et < 0 && (et = 0);
        et = Math.round(et, 0);
        f.value = et;
        ot >= 1e7 && (adjustablesurcharge = et * .15, kt = et + adjustablesurcharge, p = calcindividualTax(1e7 - wt, n, u, i) + (ot - 1e7) + bt, kt > p ? (marginalrelief = kt - p, st = adjustablesurcharge - marginalrelief) : st = adjustablesurcharge, st = Math.round(st, 0));
        ht = (et + st) * .02;
        ct = (et + st) * .01
    }
    ht = Math.round(ht, 0);
    ct = Math.round(ct, 0);
    f.value = addthousandseprator(et);
    e.value = addthousandseprator(st);
    o.value = addthousandseprator(ht);
    s.value = addthousandseprator(ct);
    h.value = addthousandseprator(et + st + ht + ct)
}

function calculateData201617(n, t, i, r, u, f, e, o, s, h, c, l, a, v, y, p, w, b, k, d, g, nt, tt, it, rt) {
    var ut = 0,
        et = 0,
        ot = 0,
        st = 0,
        ft = parseInt(getNum(r.value)),
        at = 0,
        vt = 0,
        yt, v;
    if (t == "Domestic Company") ut = ft * .3, ut = Math.round(ut, 0), ft > 1e7 && ft <= 1e8 ? (adjustablesurcharge = ut * .07, ut + adjustablesurcharge > ft - 7e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)) : ft > 1e8 && (adjustablesurcharge = ut * .12, ut + adjustablesurcharge > ft - 679e5 ? (marginalrelief = ut + adjustablesurcharge - (ft - 679e5), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Foreign Company") ut = ft * .4, ut = Math.round(ut, 0), ft > 1e7 && ft <= 1e8 ? (adjustablesurcharge = ut * .02, ut + adjustablesurcharge > ft - 6e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 6e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)) : ft > 1e8 && (adjustablesurcharge = ut * .05, ut + adjustablesurcharge > ft - 592e5 ? (marginalrelief = ut + adjustablesurcharge - (ft - 592e5), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "LLP") ut = ft * .3, ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .12, ut + adjustablesurcharge > ft - 7e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Firms") ut = ft * .3, ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .12, ut + adjustablesurcharge > ft - 7e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Co-operative Society") ft <= 1e4 ? ut = ft * .1 : ft > 1e4 && ft <= 2e4 ? ut = (ft - 1e4) * .2 + 1e3 : ft > 2e4 && (ut = (ft - 2e4) * .3 + 3e3), ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .12, ut + adjustablesurcharge > ft - 7003e3 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7003e3), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "AOPs/BOI") ft <= 25e4 ? ut = 0 : ft > 25e4 && ft <= 5e5 ? ut = (ft - 25e4) * .1 : ft > 5e5 && ft <= 1e6 ? ut = (ft - 5e5) * .2 + 25e3 : ft > 1e6 && (ut = (ft - 1e6) * .3 + 125e3), ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .12, ut + adjustablesurcharge > ft - 7175e3 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7175e3), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "HUF") ft <= 25e4 ? ut = 0 : ft > 25e4 && ft <= 5e5 ? ut = (ft - 25e4) * .1 : ft > 5e5 && ft <= 1e6 ? ut = (ft - 5e5) * .2 + 25e3 : ft > 1e6 && (ut = (ft - 1e6) * .3 + 125e3), ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .12, ut + adjustablesurcharge > ft - 7175e3 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7175e3), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Individual") {
        if (rt == !1) {
            var ht = parseInt(getNum(c.value)),
                ct = 0,
                lt = parseInt(getNum(a.value));
            ct = ht;
            ht > l && lt > 5e3 && (ht = ht + lt);
            ht = ht - (Math.min(parseInt(getNum(y.value)), v) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)));
            ct = ct - (Math.min(parseInt(getNum(y.value)), v) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)));
            at = Math.min(parseInt(getNum(y.value)), v) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value));
            ht < 0 && (ht = 0);
            ct < 0 && (ct = 0);
            k.value = addthousandseprator(ct);
            ut = ht > l && lt > 5e3 ? calcindividualTax(ht, n, u, i) - calcindividualTax(l + lt, n, u, i) : calcindividualTax(ht, n, u, i);
            ut < 0 && (ut = 0);
            ut = Math.round(ut, 0);
            d.value = addthousandseprator(ut);
            ut = ut + parseInt(getNum(g.value)) + parseInt(getNum(nt.value)) + parseInt(getNum(tt.value)) + parseInt(getNum(it.value));
            vt = parseInt(getNum(g.value)) + parseInt(getNum(nt.value)) + parseInt(getNum(tt.value)) + parseInt(getNum(it.value));
            u != "Non-Resident" && ft <= 5e5 && (ut = ut - 2e3, ut < 0 && (ut = 0))
        } else at = 0, vt = 0, ut = calcindividualTax(ft, n, u, i), u != "Non-Resident" && ft <= 5e5 && (ut = ut - 2e3, ut < 0 && (ut = 0));
        ut < 0 && (ut = 0);
        ut = Math.round(ut, 0);
        f.value = ut;
        ft >= 1e7 && (adjustablesurcharge = ut * .12, yt = ut + adjustablesurcharge, v = calcindividualTax(1e7 - at, n, u, i) + (ft - 1e7) + vt, yt > v ? (marginalrelief = yt - v, et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0));
        ot = (ut + et) * .02;
        st = (ut + et) * .01
    }
    ot = Math.round(ot, 0);
    st = Math.round(st, 0);
    f.value = addthousandseprator(ut);
    e.value = addthousandseprator(et);
    o.value = addthousandseprator(ot);
    s.value = addthousandseprator(st);
    h.value = addthousandseprator(ut + et + ot + st)
}

function calculateData201516(n, t, i, r, u, f, e, o, s, h, c, l, a, v, y, p, w, b, k, d, g, nt, tt, it, rt) {
    var ut = 0,
        et = 0,
        ot = 0,
        st = 0,
        ft = parseInt(getNum(r.value)),
        at = 0,
        vt = 0,
        yt, v;
    if (t == "Domestic Company") ut = ft * .3, ut = Math.round(ut, 0), ft > 1e7 && ft <= 1e8 ? (adjustablesurcharge = ut * .05, ut + adjustablesurcharge > ft - 7e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)) : ft > 1e8 && (adjustablesurcharge = ut * .1, ut + adjustablesurcharge > ft - 685e5 ? (marginalrelief = ut + adjustablesurcharge - (ft - 685e5), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Foreign Company") ut = ft * .4, ut = Math.round(ut, 0), ft > 1e7 && ft <= 1e8 ? (adjustablesurcharge = ut * .02, ut + adjustablesurcharge > ft - 6e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 6e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)) : ft > 1e8 && (adjustablesurcharge = ut * .05, ut + adjustablesurcharge > ft - 592e5 ? (marginalrelief = ut + adjustablesurcharge - (ft - 592e5), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "LLP") ut = ft * .3, ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .1, ut + adjustablesurcharge > ft - 7e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Firms") ut = ft * .3, ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .1, ut + adjustablesurcharge > ft - 7e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Co-operative Society") ft <= 1e4 ? ut = ft * .1 : ft > 1e4 && ft <= 2e4 ? ut = (ft - 1e4) * .2 + 1e3 : ft > 2e4 && (ut = (ft - 2e4) * .3 + 3e3), ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .1, ut + adjustablesurcharge > ft - 7003e3 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7003e3), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "AOPs/BOI") ft <= 25e4 ? ut = 0 : ft > 25e4 && ft <= 5e5 ? ut = (ft - 25e4) * .1 : ft > 5e5 && ft <= 1e6 ? ut = (ft - 5e5) * .2 + 25e3 : ft > 1e6 && (ut = (ft - 1e6) * .3 + 125e3), ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .1, ut + adjustablesurcharge > ft - 7175e3 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7175e3), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "HUF") ft <= 25e4 ? ut = 0 : ft > 25e4 && ft <= 5e5 ? ut = (ft - 25e4) * .1 : ft > 5e5 && ft <= 1e6 ? ut = (ft - 5e5) * .2 + 25e3 : ft > 1e6 && (ut = (ft - 1e6) * .3 + 125e3), ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .1, ut + adjustablesurcharge > ft - 7175e3 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7175e3), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Individual") {
        if (rt == !1) {
            var ht = parseInt(getNum(c.value)),
                ct = 0,
                lt = parseInt(getNum(a.value));
            ct = ht;
            ht > l && lt > 5e3 && (ht = ht + lt);
            ht = ht - (Math.min(parseInt(getNum(y.value)), v) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)));
            ct = ct - (Math.min(parseInt(getNum(y.value)), v) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)));
            at = Math.min(parseInt(getNum(y.value)), v) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value));
            ht < 0 && (ht = 0);
            ct < 0 && (ct = 0);
            k.value = addthousandseprator(ct);
            ut = ht > l && lt > 5e3 ? calcindividualTax(ht, n, u, i) - calcindividualTax(l + lt, n, u, i) : calcindividualTax(ht, n, u, i);
            ut < 0 && (ut = 0);
            ut = Math.round(ut, 0);
            d.value = addthousandseprator(ut);
            ut = ut + parseInt(getNum(g.value)) + parseInt(getNum(nt.value)) + parseInt(getNum(tt.value)) + parseInt(getNum(it.value));
            vt = parseInt(getNum(g.value)) + parseInt(getNum(nt.value)) + parseInt(getNum(tt.value)) + parseInt(getNum(it.value));
            u != "Non-Resident" && ft <= 5e5 && (ut = ut - 2e3, ut < 0 && (ut = 0))
        } else at = 0, vt = 0, ut = calcindividualTax(ft, n, u, i), u != "Non-Resident" && ft <= 5e5 && (ut = ut - 2e3, ut < 0 && (ut = 0));
        ut < 0 && (ut = 0);
        ut = Math.round(ut, 0);
        f.value = ut;
        ft >= 1e7 && (adjustablesurcharge = ut * .1, yt = ut + adjustablesurcharge, v = calcindividualTax(1e7 - at, n, u, i) + (ft - 1e7) + vt, yt > v ? (marginalrelief = yt - v, et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0));
        ot = (ut + et) * .02;
        st = (ut + et) * .01
    }
    ot = Math.round(ot, 0);
    st = Math.round(st, 0);
    f.value = addthousandseprator(ut);
    e.value = addthousandseprator(et);
    o.value = addthousandseprator(ot);
    s.value = addthousandseprator(st);
    h.value = addthousandseprator(ut + et + ot + st)
}

function calculateData201415(n, t, i, r, u, f, e, o, s, h, c, l, a, v, y, p, w, b, k, d, g, nt, tt, it, rt) {
    var ut = 0,
        et = 0,
        ot = 0,
        st = 0,
        ft = parseInt(getNum(r.value)),
        at = 0,
        vt = 0,
        yt, v;
    if (t == "Domestic Company") ut = ft * .3, ut = Math.round(ut, 0), ft > 1e7 && ft <= 1e8 ? (adjustablesurcharge = ut * .05, ut + adjustablesurcharge > ft - 7e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)) : ft > 1e8 && (adjustablesurcharge = ut * .1, ut + adjustablesurcharge > ft - 685e5 ? (marginalrelief = ut + adjustablesurcharge - (ft - 685e5), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Foreign Company") ut = ft * .4, ut = Math.round(ut, 0), ft > 1e7 && ft <= 1e8 ? (adjustablesurcharge = ut * .02, ut + adjustablesurcharge > ft - 6e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 6e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)) : ft > 1e8 && (adjustablesurcharge = ut * .05, ut + adjustablesurcharge > ft - 592e5 ? (marginalrelief = ut + adjustablesurcharge - (ft - 592e5), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "LLP") ut = ft * .3, ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .1, ut + adjustablesurcharge > ft - 7e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Firms") ut = ft * .3, ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .1, ut + adjustablesurcharge > ft - 7e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Co-operative Society") ft <= 1e4 ? ut = ft * .1 : ft > 1e4 && ft <= 2e4 ? ut = (ft - 1e4) * .2 + 1e3 : ft > 2e4 && (ut = (ft - 2e4) * .3 + 3e3), ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .1, ut + adjustablesurcharge > ft - 7003e3 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7003e3), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "AOPs/BOI") ft <= 2e5 ? ut = 0 : ft > 2e5 && ft <= 5e5 ? ut = (ft - 2e5) * .1 : ft > 5e5 && ft <= 1e6 ? ut = (ft - 5e5) * .2 + 3e4 : ft > 1e6 && (ut = (ft - 1e6) * .3 + 13e4), ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .1, ut + adjustablesurcharge > ft - 717e4 ? (marginalrelief = ut + adjustablesurcharge - (ft - 717e4), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "HUF") ft <= 2e5 ? ut = 0 : ft > 2e5 && ft <= 5e5 ? ut = (ft - 2e5) * .1 : ft > 5e5 && ft <= 1e6 ? ut = (ft - 5e5) * .2 + 3e4 : ft > 1e6 && (ut = (ft - 1e6) * .3 + 13e4), ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .1, ut + adjustablesurcharge > ft - 717e4 ? (marginalrelief = ut + adjustablesurcharge - (ft - 717e4), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Individual") {
        if (rt == !1) {
            var ht = parseInt(getNum(c.value)),
                ct = 0,
                lt = parseInt(getNum(a.value));
            ct = ht;
            ht > l && lt > 5e3 && (ht = ht + lt);
            ht = ht - (Math.min(parseInt(getNum(y.value)), v) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)));
            ct = ct - (Math.min(parseInt(getNum(y.value)), v) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)));
            at = Math.min(parseInt(getNum(y.value)), v) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value));
            ht < 0 && (ht = 0);
            ct < 0 && (ct = 0);
            k.value = addthousandseprator(ct);
            ut = ht > l && lt > 5e3 ? calcindividualTax(ht, n, u, i) - calcindividualTax(l + lt, n, u, i) : calcindividualTax(ht, n, u, i);
            ut < 0 && (ut = 0);
            ut = Math.round(ut, 0);
            d.value = addthousandseprator(ut);
            ut = ut + parseInt(getNum(g.value)) + parseInt(getNum(nt.value)) + parseInt(getNum(tt.value)) + parseInt(getNum(it.value));
            vt = parseInt(getNum(g.value)) + parseInt(getNum(nt.value)) + parseInt(getNum(tt.value)) + parseInt(getNum(it.value));
            u != "Non-Resident" && ft <= 5e5 && (ut = ut - 2e3, ut < 0 && (ut = 0))
        } else at = 0, vt = 0, ut = calcindividualTax(ft, n, u, i), u != "Non-Resident" && ft <= 5e5 && (ut = ut - 2e3, ut < 0 && (ut = 0));
        ut < 0 && (ut = 0);
        ut = Math.round(ut, 0);
        f.value = ut;
        ft >= 1e7 && (adjustablesurcharge = ut * .1, yt = ut + adjustablesurcharge, v = calcindividualTax(1e7 - at, n, u, i) + (ft - 1e7) + vt, yt > v ? (marginalrelief = yt - v, et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0));
        ot = (ut + et) * .02;
        st = (ut + et) * .01
    }
    ot = Math.round(ot, 0);
    st = Math.round(st, 0);
    f.value = addthousandseprator(ut);
    e.value = addthousandseprator(et);
    o.value = addthousandseprator(ot);
    s.value = addthousandseprator(st);
    h.value = addthousandseprator(ut + et + ot + st)
}

function calculateData201314(n, t, i, r, u, f, e, o, s, h, c, l, a, v, y, p, w, b, k, d, g, nt, tt, it, rt) {
    var ut = 0,
        et = 0,
        ot = 0,
        st = 0,
        ft = parseInt(getNum(r.value));
    if (t == "Domestic Company") ut = ft * .3, ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .05, ut + adjustablesurcharge > ft - 7e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Foreign Company") ut = ft * .4, ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .02, ut + adjustablesurcharge > ft - 6e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 6e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "LLP") ut = ft * .3, ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Firms") ut = ft * .3, ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Co-operative Society") ft <= 1e4 ? ut = ft * .1 : ft > 1e4 && ft <= 2e4 ? ut = (ft - 1e4) * .2 + 1e3 : ft > 2e4 && (ut = (ft - 2e4) * .3 + 3e3), ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "AOPs/BOI") ft <= 2e5 ? ut = 0 : ft > 2e5 && ft <= 5e5 ? ut = (ft - 2e5) * .1 : ft > 5e5 && ft <= 1e6 ? ut = (ft - 5e5) * .2 + 3e4 : ft > 1e6 && (ut = (ft - 1e6) * .3 + 13e4), ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "HUF") ft <= 2e5 ? ut = 0 : ft > 2e5 && ft <= 5e5 ? ut = (ft - 2e5) * .1 : ft > 5e5 && ft <= 1e6 ? ut = (ft - 5e5) * .2 + 3e4 : ft > 1e6 && (ut = (ft - 1e6) * .3 + 13e4), ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Individual") {
        if (rt == !1) {
            var ct = 0,
                ht = parseInt(getNum(c.value)),
                lt = parseInt(getNum(a.value));
            ct = ht;
            ht > l && lt > 5e3 && (ht = ht + lt);
            ht = ht - (Math.min(parseInt(getNum(y.value)), v) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)));
            ct = ct - (Math.min(parseInt(getNum(y.value)), v) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)));
            ht < 0 && (ht = 0);
            ct < 0 && (ct = 0);
            k.value = addthousandseprator(ct);
            ut = ht > l && lt > 5e3 ? calcindividualTax(ht, n, u, i) - calcindividualTax(l + lt, n, u, i) : calcindividualTax(ht, n, u, i);
            ut < 0 && (ut = 0);
            ut = Math.round(ut, 0);
            d.value = addthousandseprator(ut);
            ut = ut + parseInt(getNum(g.value)) + parseInt(getNum(nt.value)) + parseInt(getNum(tt.value)) + parseInt(getNum(it.value))
        } else ut = calcindividualTax(ft, n, u, i), ut < 0 && (ut = 0), ut = Math.round(ut, 0);
        f.value = ut;
        et = 0;
        ot = (ut + et) * .02;
        st = (ut + et) * .01
    }
    ot = Math.round(ot, 0);
    st = Math.round(st, 0);
    f.value = addthousandseprator(ut);
    e.value = addthousandseprator(et);
    o.value = addthousandseprator(ot);
    s.value = addthousandseprator(st);
    h.value = addthousandseprator(ut + et + ot + st)
}

function calculateData201213(n, t, i, r, u, f, e, o, s, h, c, l, a, v, y, p, w, b, k, d, g, nt, tt, it, rt) {
    var ut = 0,
        et = 0,
        ot = 0,
        st = 0,
        ft = parseInt(getNum(r.value));
    if (t == "Domestic Company") ut = ft * .3, ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .05, ut + adjustablesurcharge > ft - 7e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Foreign Company") ut = ft * .4, ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .02, ut + adjustablesurcharge > ft - 6e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 6e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "LLP") ut = ft * .3, ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Firms") ut = ft * .3, ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Co-operative Society") ft <= 1e4 ? ut = ft * .1 : ft > 1e4 && ft <= 2e4 ? ut = (ft - 1e4) * .2 + 1e3 : ft > 2e4 && (ut = (ft - 2e4) * .3 + 3e3), ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "AOPs/BOI") ft <= 18e4 ? ut = 0 : ft > 18e4 && ft <= 5e5 ? ut = (ft - 18e4) * .1 : ft > 5e5 && ft <= 8e5 ? ut = (ft - 5e5) * .2 + 32e3 : ft > 8e5 && (ut = (ft - 8e5) * .3 + 92e3), ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "HUF") ft <= 18e4 ? ut = 0 : ft > 18e4 && ft <= 5e5 ? ut = (ft - 18e4) * .1 : ft > 5e5 && ft <= 8e5 ? ut = (ft - 5e5) * .2 + 32e3 : ft > 8e5 && (ut = (ft - 8e5) * .3 + 92e3), ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Individual") {
        if (rt == !1) {
            var ct = 0,
                ht = parseInt(getNum(c.value)),
                lt = parseInt(getNum(a.value));
            ct = ht;
            ht > l && lt > 5e3 && (ht = ht + lt);
            ht = ht - (Math.min(parseInt(getNum(y.value)), v) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)));
            ct = ct - (Math.min(parseInt(getNum(y.value)), v) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)));
            ht < 0 && (ht = 0);
            ct < 0 && (ct = 0);
            k.value = addthousandseprator(ct);
            ut = ht > l && lt > 5e3 ? calcindividualTax(ht, n, u, i) - calcindividualTax(l + lt, n, u, i) : calcindividualTax(ht, n, u, i);
            ut < 0 && (ut = 0);
            ut = Math.round(ut, 0);
            d.value = addthousandseprator(ut);
            ut = ut + parseInt(getNum(g.value)) + parseInt(getNum(nt.value)) + parseInt(getNum(tt.value)) + parseInt(getNum(it.value))
        } else ut = calcindividualTax(ft, n, u, i), ut < 0 && (ut = 0), ut = Math.round(ut, 0);
        f.value = ut;
        et = 0;
        ot = (ut + et) * .02;
        st = (ut + et) * .01
    }
    ot = Math.round(ot, 0);
    st = Math.round(st, 0);
    f.value = addthousandseprator(ut);
    e.value = addthousandseprator(et);
    o.value = addthousandseprator(ot);
    s.value = addthousandseprator(st);
    h.value = addthousandseprator(ut + et + ot + st)
}

function calculateData201112(n, t, i, r, u, f, e, o, s, h, c, l, a, v, y, p, w, b, k, d, g, nt, tt, it, rt) {
    var ut = 0,
        et = 0,
        ot = 0,
        st = 0,
        ft = parseInt(getNum(r.value));
    if (t == "Domestic Company") ut = ft * .3, ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .075, ut + adjustablesurcharge > ft - 7e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 7e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Foreign Company") ut = ft * .4, ut = Math.round(ut, 0), ft >= 1e7 && (adjustablesurcharge = ut * .025, ut + adjustablesurcharge > ft - 6e6 ? (marginalrelief = ut + adjustablesurcharge - (ft - 6e6), et = adjustablesurcharge - marginalrelief) : et = adjustablesurcharge, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "LLP") ut = ft * .3, ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Firms") ut = ft * .3, ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Co-operative Society") ft <= 1e4 ? ut = ft * .1 : ft > 1e4 && ft <= 2e4 ? ut = (ft - 1e4) * .2 + 1e3 : ft > 2e4 && (ut = (ft - 2e4) * .3 + 3e3), ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "AOPs/BOI") ft <= 16e4 ? ut = 0 : ft > 16e4 && ft <= 5e5 ? ut = (ft - 16e4) * .1 : ft > 5e5 && ft <= 8e5 ? ut = (ft - 5e5) * .2 + 34e3 : ft > 8e5 && (ut = (ft - 8e5) * .3 + 94e3), ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "HUF") ft <= 16e4 ? ut = 0 : ft > 16e4 && ft <= 5e5 ? ut = (ft - 16e4) * .1 : ft > 5e5 && ft <= 8e5 ? ut = (ft - 5e5) * .2 + 34e3 : ft > 8e5 && (ut = (ft - 8e5) * .3 + 94e3), ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Individual") {
        if (rt == !1) {
            var ct = 0,
                ht = parseInt(getNum(c.value)),
                lt = parseInt(getNum(a.value));
            ct = ht;
            ht > l && lt > 5e3 && (ht = ht + lt);
            ht = ht - (Math.min(parseInt(getNum(y.value)), v) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)));
            ct = ct - (Math.min(parseInt(getNum(y.value)), v) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)));
            ht < 0 && (ht = 0);
            ct < 0 && (ct = 0);
            k.value = addthousandseprator(ct);
            ut = ht > l && lt > 5e3 ? calcindividualTax(ht, n, u, i) - calcindividualTax(l + lt, n, u, i) : calcindividualTax(ht, n, u, i);
            ut < 0 && (ut = 0);
            ut = Math.round(ut, 0);
            d.value = addthousandseprator(ut);
            ut = ut + parseInt(getNum(g.value)) + parseInt(getNum(nt.value)) + parseInt(getNum(tt.value)) + parseInt(getNum(it.value))
        } else ut = calcindividualTax(ft, n, u, i), ut < 0 && (ut = 0), ut = Math.round(ut, 0), f.value = ut;
        et = 0;
        ot = (ut + et) * .02;
        st = (ut + et) * .01
    }
    ot = Math.round(ot, 0);
    st = Math.round(st, 0);
    f.value = addthousandseprator(ut);
    e.value = addthousandseprator(et);
    o.value = addthousandseprator(ot);
    s.value = addthousandseprator(st);
    h.value = addthousandseprator(ut + et + ot + st)
}

function calculateData201011(n, t, i, r, u, f, e, o, s, h, c, l, a, v, y, p, w, b, k, d, g, nt, tt, it, rt) {
    var ut = 0,
        et = 0,
        ot = 0,
        st = 0,
        at = 0,
        ct = 0,
        ft = parseInt(getNum(r.value));
    if (t == "Domestic Company") ut = ft * .3, ut = Math.round(ut, 0), ft >= 1e7 && (ct = ut * .1, ut + ct > ft - 7e6 ? (at = ut + ct - (ft - 7e6), et = ct - at) : et = ct, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Foreign Company") ut = ft * .4, ut = Math.round(ut, 0), ft >= 1e7 && (ct = ut * .025, ut + ct > ft - 6e6 ? (at = ut + ct - (ft - 6e6), et = ct - at) : et = ct, et = Math.round(et, 0)), ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "LLP") ut = ft * .3, ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Firms") ut = ft * .3, ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Co-operative Society") ft <= 1e4 ? ut = ft * .1 : ft > 1e4 && ft <= 2e4 ? ut = (ft - 1e4) * .2 + 1e3 : ft > 2e4 && (ut = (ft - 2e4) * .3 + 3e3), ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "AOPs/BOI") ft <= 16e4 ? ut = 0 : ft > 16e4 && ft <= 3e5 ? ut = (ft - 16e4) * .1 : ft > 3e5 && ft <= 5e5 ? ut = (ft - 3e5) * .2 + 14e3 : ft > 5e5 && (ut = (ft - 5e5) * .3 + 54e3), ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "HUF") ft <= 16e4 ? ut = 0 : ft > 16e4 && ft <= 3e5 ? ut = (ft - 16e4) * .1 : ft > 3e5 && ft <= 5e5 ? ut = (ft - 3e5) * .2 + 14e3 : ft > 5e5 && (ut = (ft - 5e5) * .3 + 54e3), ut = Math.round(ut, 0), et = 0, ot = (ut + et) * .02, st = (ut + et) * .01;
    else if (t == "Individual") {
        if (rt == !1) {
            var lt = 0,
                ht = parseInt(getNum(c.value)),
                vt = parseInt(getNum(a.value));
            lt = ht;
            ht > l && vt > 5e3 && (ht = ht + vt);
            ht = ht - (Math.min(parseInt(getNum(y.value)), v) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)));
            lt = lt - (Math.min(parseInt(getNum(y.value)), v) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)));
            ht < 0 && (ht = 0);
            lt < 0 && (lt = 0);
            k.value = addthousandseprator(lt);
            ut = ht > l && vt > 5e3 ? calcindividualTax(ht, n, u, i) - calcindividualTax(l + vt, n, u, i) : calcindividualTax(ht, n, u, i);
            ut < 0 && (ut = 0);
            ut = Math.round(ut, 0);
            d.value = addthousandseprator(ut);
            ut = ut + parseInt(getNum(g.value)) + parseInt(getNum(nt.value)) + parseInt(getNum(tt.value)) + parseInt(getNum(it.value))
        } else ut = calcindividualTax(ft, n, u, i), ut < 0 && (ut = 0), ut = Math.round(ut, 0);
        f.value = ut;
        et = 0;
        ot = (ut + et) * .02;
        st = (ut + et) * .01
    }
    ot = Math.round(ot, 0);
    st = Math.round(st, 0);
    f.value = addthousandseprator(ut);
    e.value = addthousandseprator(et);
    o.value = addthousandseprator(ot);
    s.value = addthousandseprator(st);
    h.value = addthousandseprator(ut + et + ot + st)
}

function calcindividualTax(n, t, i, r) {
    try {
        if (n < 0) return n = 0, 0;
        var u = 0;
        return t == "2010-11" && (r == "Female" && (i == "Resident" || i == "Not Ordinary Resident") ? n <= 19e4 ? u = 0 : n > 19e4 && n <= 3e5 ? u = (n - 19e4) * .1 : n > 3e5 && n <= 5e5 ? u = (n - 3e5) * .2 + 11e3 : n > 5e5 && (u = (n - 5e5) * .3 + 51e3) : r == "Senior Citizen" && (i == "Resident" || i == "Not Ordinary Resident") ? n <= 24e4 ? u = 0 : n > 24e4 && n <= 3e5 ? u = (n - 24e4) * .1 : n > 3e5 && n <= 5e5 ? u = (n - 3e5) * .2 + 6e3 : n > 5e5 && (u = (n - 5e5) * .3 + 46e3) : n <= 16e4 ? u = 0 : n > 16e4 && n <= 3e5 ? u = (n - 16e4) * .1 : n > 3e5 && n <= 5e5 ? u = (n - 3e5) * .2 + 14e3 : n > 5e5 && (u = (n - 5e5) * .3 + 54e3)), t == "2011-12" && (r == "Female" && (i == "Resident" || i == "Not Ordinary Resident") ? n <= 19e4 ? u = 0 : n > 19e4 && n <= 5e5 ? u = (n - 19e4) * .1 : n > 5e5 && n <= 8e5 ? u = (n - 5e5) * .2 + 31e3 : n > 8e5 && (u = (n - 8e5) * .3 + 91e3) : r == "Senior Citizen" && (i == "Resident" || i == "Not Ordinary Resident") ? n <= 24e4 ? u = 0 : n > 24e4 && n <= 5e5 ? u = (n - 24e4) * .1 : n > 5e5 && n <= 8e5 ? u = (n - 5e5) * .2 + 26e3 : n > 8e5 && (u = (n - 8e5) * .3 + 86e3) : n <= 16e4 ? u = 0 : n > 16e4 && n <= 5e5 ? u = (n - 16e4) * .1 : n > 5e5 && n <= 8e5 ? u = (n - 5e5) * .2 + 34e3 : n > 8e5 && (u = (n - 8e5) * .3 + 94e3)), t == "2012-13" && (r == "Female" && (i == "Resident" || i == "Not Ordinary Resident") ? n <= 19e4 ? u = 0 : n > 19e4 && n <= 5e5 ? u = (n - 19e4) * .1 : n > 5e5 && n <= 8e5 ? u = (n - 5e5) * .2 + 31e3 : n > 8e5 && (u = (n - 8e5) * .3 + 91e3) : r == "Senior Citizen" && (i == "Resident" || i == "Not Ordinary Resident") ? n <= 25e4 ? u = 0 : n > 25e4 && n <= 5e5 ? u = (n - 25e4) * .1 : n > 5e5 && n <= 8e5 ? u = (n - 5e5) * .2 + 25e3 : n > 8e5 && (u = (n - 8e5) * .3 + 85e3) : r == "Super Senior Citizen" && (i == "Resident" || i == "Not Ordinary Resident") ? n <= 5e5 ? u = 0 : n > 5e5 && n <= 8e5 ? u = (n - 5e5) * .2 : n > 8e5 && (u = (n - 8e5) * .3 + 6e4) : n <= 18e4 ? u = 0 : n > 18e4 && n <= 5e5 ? u = (n - 18e4) * .1 : n > 5e5 && n <= 8e5 ? u = (n - 5e5) * .2 + 32e3 : n > 8e5 && (u = (n - 8e5) * .3 + 92e3)), t == "2013-14" && (r == "Senior Citizen" && (i == "Resident" || i == "Not Ordinary Resident") ? n <= 25e4 ? u = 0 : n > 25e4 && n <= 5e5 ? u = (n - 25e4) * .1 : n > 5e5 && n <= 1e6 ? u = (n - 5e5) * .2 + 25e3 : n > 1e6 && (u = (n - 1e6) * .3 + 125e3) : r == "Super Senior Citizen" && (i == "Resident" || i == "Not Ordinary Resident") ? n <= 5e5 ? u = 0 : n > 5e5 && n <= 1e6 ? u = (n - 5e5) * .2 : n > 1e6 && (u = (n - 1e6) * .3 + 1e5) : n <= 2e5 ? u = 0 : n > 2e5 && n <= 5e5 ? u = (n - 2e5) * .1 : n > 5e5 && n <= 1e6 ? u = (n - 5e5) * .2 + 3e4 : n > 1e6 && (u = (n - 1e6) * .3 + 13e4)), t == "2014-15" && (r == "Senior Citizen" && (i == "Resident" || i == "Not Ordinary Resident") ? n <= 25e4 ? u = 0 : n > 25e4 && n <= 5e5 ? u = (n - 25e4) * .1 : n > 5e5 && n <= 1e6 ? u = (n - 5e5) * .2 + 25e3 : n > 1e6 && (u = (n - 1e6) * .3 + 125e3) : r == "Super Senior Citizen" && (i == "Resident" || i == "Not Ordinary Resident") ? n <= 5e5 ? u = 0 : n > 5e5 && n <= 1e6 ? u = (n - 5e5) * .2 : n > 1e6 && (u = (n - 1e6) * .3 + 1e5) : n <= 2e5 ? u = 0 : n > 2e5 && n <= 5e5 ? u = (n - 2e5) * .1 : n > 5e5 && n <= 1e6 ? u = (n - 5e5) * .2 + 3e4 : n > 1e6 && (u = (n - 1e6) * .3 + 13e4)), t == "2015-16" && (r == "Senior Citizen" && (i == "Resident" || i == "Not Ordinary Resident") ? n <= 3e5 ? u = 0 : n > 3e5 && n <= 5e5 ? u = (n - 3e5) * .1 : n > 5e5 && n <= 1e6 ? u = (n - 5e5) * .2 + 2e4 : n > 1e6 && (u = (n - 1e6) * .3 + 12e4) : r == "Super Senior Citizen" && (i == "Resident" || i == "Not Ordinary Resident") ? n <= 5e5 ? u = 0 : n > 5e5 && n <= 1e6 ? u = (n - 5e5) * .2 : n > 1e6 && (u = (n - 1e6) * .3 + 1e5) : n <= 25e4 ? u = 0 : n > 25e4 && n <= 5e5 ? u = (n - 25e4) * .1 : n > 5e5 && n <= 1e6 ? u = (n - 5e5) * .2 + 25e3 : n > 1e6 && (u = (n - 1e6) * .3 + 125e3)), t == "2016-17" && (r == "Senior Citizen" && (i == "Resident" || i == "Not Ordinary Resident") ? n <= 3e5 ? u = 0 : n > 3e5 && n <= 5e5 ? u = (n - 3e5) * .1 : n > 5e5 && n <= 1e6 ? u = (n - 5e5) * .2 + 2e4 : n > 1e6 && (u = (n - 1e6) * .3 + 12e4) : r == "Super Senior Citizen" && (i == "Resident" || i == "Not Ordinary Resident") ? n <= 5e5 ? u = 0 : n > 5e5 && n <= 1e6 ? u = (n - 5e5) * .2 : n > 1e6 && (u = (n - 1e6) * .3 + 1e5) : n <= 25e4 ? u = 0 : n > 25e4 && n <= 5e5 ? u = (n - 25e4) * .1 : n > 5e5 && n <= 1e6 ? u = (n - 5e5) * .2 + 25e3 : n > 1e6 && (u = (n - 1e6) * .3 + 125e3)), t == "2017-18" && (r == "Senior Citizen" && (i == "Resident" || i == "Not Ordinary Resident") ? n <= 3e5 ? u = 0 : n > 3e5 && n <= 5e5 ? u = (n - 3e5) * .1 : n > 5e5 && n <= 1e6 ? u = (n - 5e5) * .2 + 2e4 : n > 1e6 && (u = (n - 1e6) * .3 + 12e4) : r == "Super Senior Citizen" && (i == "Resident" || i == "Not Ordinary Resident") ? n <= 5e5 ? u = 0 : n > 5e5 && n <= 1e6 ? u = (n - 5e5) * .2 : n > 1e6 && (u = (n - 1e6) * .3 + 1e5) : n <= 25e4 ? u = 0 : n > 25e4 && n <= 5e5 ? u = (n - 25e4) * .1 : n > 5e5 && n <= 1e6 ? u = (n - 5e5) * .2 + 25e3 : n > 1e6 && (u = (n - 1e6) * .3 + 125e3)), u
    } catch (f) {
        alert("An error has occurred: Module-IndividaulTax :" + f.message)
    }
}

function calcHPjs(n, t, i, r, u, f, e, o, s, h, c) {
    t.value = addthousandseprator(parseInt(getNum(i.value)) * -1);
    var l = parseInt(getNum(t.value));
    r.value = addthousandseprator(parseInt(getNum(u.value)) - (parseInt(getNum(f.value)) + parseInt(getNum(e.value))));
    o.value = addthousandseprator(parseInt(getNum(r.value)) * .3);
    o.value < 0 && (o.value = 0);
    s.value = addthousandseprator(parseInt(getNum(r.value)) - (parseInt(getNum(o.value)) + parseInt(getNum(h.value))));
    n == "2015-16" || n == "2016-17" || n == "2017-18" ? l < -2e5 && (l = -2e5) : l < -15e4 && (l = -15e4);
    c.value = addthousandseprator(l + parseInt(getNum(s.value)))
}

function calcCGjs(n, t, i, r, u) {
    n.value = addthousandseprator(parseInt(getNum(t.value)) + parseInt(getNum(i.value)) + parseFloat(getNum(r.value)) + parseFloat(getNum(u.value)))
}

function calcOSjs(n, t, i, r) {
    n.value = addthousandseprator(parseInt(getNum(t.value)) + parseFloat(getNum(i.value)) + parseFloat(getNum(r.value)))
}

function calcdeductionjs(n, t, i, r, u, f, e, o, s, h, c, l, a, v, y, p, w, b, k, d, g, nt, tt, it, rt, ut, ft, et, ot, st, ht) {
    var lt = 0,
        ni = 0,
        ui = n.value,
        ct = t.value,
        kt = parseInt(getNum(i.value)),
        yt = parseInt(getNum(r.value)),
        bt = parseInt(getNum(u.value)),
        dt = parseInt(getNum(f.value)),
        pt = parseInt(getNum(e.value)),
        ti = parseInt(getNum(o.value)),
        ii = parseInt(getNum(s.value)),
        gt = parseInt(getNum(et.value)),
        wt = parseInt(getNum(ot.value)),
        ri = parseInt(getNum(st.value)),
        at, vt;
    //ct == "2016-17" || ct == "2017-18" ? gt > 5e4 && (gt = 5e4) : gt = 0;
    ct == "2016-17" || ct == "2017-18" ? wt > 8e4 && (wt = 8e4) : wt > 6e4 && (wt = 6e4);
    kt > 2e4 && (kt = 2e4);
    ct == "2014-15" || ct == "2015-16" || ct == "2016-17" || ct == "2017-18" ? ni = 12e5 : ct == "2013-14" && (ni = 1e6);
    yt = ht <= ni ? yt * .5 > 25000 ? 25000 : Math.round(yt * .5) : 0;
    ct == "2016-17" || ct == "2017-18" ? bt > 6e4 && (bt = 6e4) : bt > 4e4 && (bt = 4e4);
    dt > 1e4 && (dt = 1e4);
    (ct == "2010-11" || ct == "2011-12" || ct == "2012-13") && (dt = 0, yt = 0);
    ct != "2011-12" && ct != "2012-13" && (kt = 0);
    ui == "Non-Resident" && (ti = 0, ii = 0, yt = 0, wt = 0);
   // ct != "2014-15" && ct != "2015-16" && ct != "2017-18" && (pt = 0);
   // ct == "2017-18" ? pt > 5e4 && (pt = 5e4) : pt > 1e5 && (pt = 1e5);
    ct == "2012-13" || ct == "2013-14" || ct == "2014-15" ? (lt = parseInt(getNum(h.value)) + parseInt(getNum(c.value)) + parseInt(getNum(l.value)) + parseInt(getNum(a.value)) + parseInt(getNum(v.value)) + parseInt(getNum(y.value)) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)) + parseInt(getNum(k.value)) + parseInt(getNum(d.value)) + parseInt(getNum(g.value)), lt > 1e5 && (lt = 1e5), lt = lt + parseInt(getNum(nt.value))) : ct == "2015-16" ? (at = getNum(g.value), at > 1e5 && (at = 1e5), vt = parseInt(getNum(k.value)), vt > 1e5 && (vt = 1e5), lt = parseInt(getNum(h.value)) + parseInt(getNum(c.value)) + parseInt(getNum(l.value)) + parseInt(getNum(a.value)) + parseInt(getNum(v.value)) + parseInt(getNum(y.value)) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)) + parseInt(getNum(vt)) + parseInt(getNum(d.value)) + parseInt(getNum(at)), lt > 15e4 && (lt = 15e4), lt = lt + parseInt(getNum(nt.value))) : ct == "2016-17" ? (at = getNum(g.value), at > 15e4 && (at = 15e4), vt = parseInt(getNum(k.value)), vt > 15e4 && (vt = 15e4), lt = parseInt(getNum(h.value)) + parseInt(getNum(c.value)) + parseInt(getNum(l.value)) + parseInt(getNum(a.value)) + parseInt(getNum(v.value)) + parseInt(getNum(y.value)) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)) + parseInt(getNum(vt)) + parseInt(getNum(d.value)) + parseInt(getNum(at)) + parseInt(getNum(ri)), lt > 15e4 && (lt = 15e4), lt = lt + parseInt(getNum(nt.value))) : ct == "2017-18" ? (at = getNum(g.value), at > 15e4 && (at = 15e4), vt = parseInt(getNum(k.value)), vt > 15e4 && (vt = 15e4), lt = parseInt(getNum(h.value)) + parseInt(getNum(c.value)) + parseInt(getNum(l.value)) + parseInt(getNum(a.value)) + parseInt(getNum(v.value)) + parseInt(getNum(y.value)) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)) + parseInt(getNum(vt)) + parseInt(getNum(d.value)) + parseInt(getNum(at)) + parseInt(getNum(ri)), lt > 15e4 && (lt = 15e4), lt = lt + parseInt(getNum(nt.value))) : (lt = parseInt(getNum(h.value)) + parseInt(getNum(c.value)) + parseInt(getNum(l.value)) + parseInt(getNum(a.value)) + parseInt(getNum(v.value)) + parseInt(getNum(y.value)) + parseInt(getNum(p.value)) + parseInt(getNum(w.value)) + parseInt(getNum(b.value)) + parseInt(getNum(k.value)) + parseInt(getNum(g.value)) + parseInt(getNum(d.value)) + parseInt(getNum(nt.value)), lt > 1e5 && (lt = 1e5));
    tt.value = addthousandseprator(lt + kt + yt + gt + ri);
    it.value = addthousandseprator(parseInt(getNum(tt.value)) + bt + dt + parseInt(getNum(rt.value)) + ti + parseInt(getNum(ut.value)) + ii + pt + wt + parseInt(getNum(ft.value)))
}

function calc80ddjs(n, t, i, r) {
    try {
        var u = n.value;
        r.value = t.checked == !0 ? i.checked == !0 ? u == "2017-18" || u == "2016-17" ? addthousandseprator(125e3) : addthousandseprator(1e5) : u == "2017-18" || u == "2016-17" ? addthousandseprator(75e3) : addthousandseprator(5e4) : 0
    } catch (f) {
        alert("An error has occurred: Module-80DD :" + f.message)
    }
}

function calc80ujs(n, t, i, r) {
    try {
        var u = n.value;
        r.value = t.checked == !0 ? i.checked == !0 ? u == "2017-18" || u == "2016-17" ? addthousandseprator(125e3) : addthousandseprator(1e5) : u == "2017-18" || u == "2016-17" ? addthousandseprator(75e3) : addthousandseprator(5e4) : 0
    } catch (f) {
        alert("An error has occurred: Module-80U :" + f.message)
    }
}

function calcTotalIncomejs(n, t, i, r, u, f) {
    try {
        var e = 0,
            o = 0;
        return o = parseInt(getNum(n.value)), e = f - (parseInt(getNum(t.value)) + parseInt(getNum(i.value)) + parseInt(getNum(r.value))), e < o && (e = o), y = parseInt(getNum(u.value)), e > y ? f - y : f - e
    } catch (s) {
        alert("An error has occurred: Module-calcTotalIncome:" + s.message)
    }
}

function exemptionlimit(n, t, i) {
    try {
        if (t == "2010-11" || t == "2011-12") return i == "Female" && (n == "Resident" || n == "Not Ordinary Resident") ? 19e4 : i == "Senior Citizen" && (n == "Resident" || n == "Not Ordinary Resident") ? 24e4 : 16e4;
        if (t == "2012-13") return i == "Female" && (n == "Resident" || n == "Not Ordinary Resident") ? 19e4 : i == "Senior Citizen" && (n == "Resident" || n == "Not Ordinary Resident") ? 25e4 : i == "Super Senior Citizen" && (n == "Resident" || n == "Not Ordinary Resident") ? 5e5 : 18e4;
        if (t == "2013-14" || t == "2014-15") return i == "Senior Citizen" && (n == "Resident" || n == "Not Ordinary Resident") ? 25e4 : i == "Super Senior Citizen" && (n == "Resident" || n == "Not Ordinary Resident") ? 5e5 : 2e5;
        if (t == "2015-16" || t == "2016-17" || t == "2017-18") return i == "Senior Citizen" && (n == "Resident" || n == "Not Ordinary Resident") ? 3e5 : i == "Super Senior Citizen" && (n == "Resident" || n == "Not Ordinary Resident") ? 5e5 : 25e4
    } catch (r) {
        alert("An error has occurred: Module-ExemptionLimit:" + r.message)
    }
}

function calcYjs(n, t, i, r, u, f) {
    var o = parseInt(getNum(n.value)),
        s = parseInt(getNum(t.value)),
        e = parseInt(getNum(i.value)),
        h = parseInt(getNum(r.value)) + parseInt(getNum(u.value));
    return f - (e + Math.min(h + o + s, f - e))
}

function calcSpecialIncomeTaxjs(n, t, i, r, u, f, e, o, s, h, c, l, a, v, y, p, w) {
    try {
        var d = parseInt(getNum(n.value)),
            b = 0,
            g = parseInt(getNum(t.value)),
            k = parseInt(getNum(i.value)) - parseInt(getNum(y)),
            nt = parseInt(getNum(r.value)) + parseInt(getNum(u.value));
        k < 0 && (k = 0);
        f.value = d;
        w == "Resident" || w == "Not Ordinary Resident" ? (e.value = addthousandseprator(Math.min(v, g, k)), b = Math.min(k, nt, p - d)) : (e.value = addthousandseprator(Math.min(v, g)), b = Math.min(nt, p - d));
        o.value = addthousandseprator(Math.min(b, parseInt(getNum(u.value))));
        s.value = addthousandseprator(b - parseInt(getNum(o.value)));
        h.value = addthousandseprator(Math.round(parseInt(getNum(f.value)) * .3, 0));
        c.value = addthousandseprator(Math.round(parseInt(getNum(o.value)) * .1, 0));
        l.value = addthousandseprator(Math.round(parseInt(getNum(e.value)) * .15, 0));
        a.value = addthousandseprator(Math.round(parseInt(getNum(s.value)) * .2, 0))
    } catch (tt) {
        alert("An error has occurred: Module-Special Income:" + tt.message)
    }
}

function calculateDataDFjs(n, t, i, r, u, f, e) {
    var o = n.value,
        s = t.value;
    o == "2013-14" ? calculateDataDF201314(t, i, r, u) : o == "2014-15" ? calculateDataDF201415(t, i, r, u) : o == "2015-16" ? calculateDataDF201516(t, i, r, u) : o == "2016-17" ? (calculateDataDF201617Current(t, i, r, u), calculateDataDF201617Previous(t, i, r, u)) : o == "2017-18" && (calculateDataDF201718Current(t, i, r, u, f, e), calculateDataDF201718Previous(t, i, r, u))
}

function calculateDataDF201718Current(n, t, i, r, u, f) {
    var l = n.value,
        e = 0,
        s = 0,
        h = 0,
        c = 0,
        y = 0,
        o = parseInt(getNum(t.value)),
        a, v, p;
    l == "Domestic Company" ? (a = 0, v = 0, f.checked == !0 ? (e = o * .29, a = 29e5, v = 29e6) : u.checked == !0 ? (e = o * .25, a = 25e5, v = 25e6) : (e = o * .3, a = 3e6, v = 3e7), e = Math.round(e, 0), o > 1e7 && o <= 1e8 ? (adjustablesurcharge = e * .07, e + adjustablesurcharge > a + (o - 1e7) ? (marginalrelief = e + adjustablesurcharge - (a + (o - 1e7)), s = adjustablesurcharge - marginalrelief) : s = adjustablesurcharge, s = Math.round(s, 0)) : o > 1e8 && (adjustablesurcharge = e * .12, e + adjustablesurcharge > v * 1.07 + (o - 1e8) ? (marginalrelief = e + adjustablesurcharge - (v * 1.07 + (o - 1e8)), s = adjustablesurcharge - marginalrelief) : s = adjustablesurcharge, s = Math.round(s, 0)), h = (e + s) * .02, c = (e + s) * .01) : l == "Foreign Company" ? (e = o * .4, e = Math.round(e, 0), o > 1e7 && o <= 1e8 ? (adjustablesurcharge = e * .02, e + adjustablesurcharge > o - 6e6 ? (marginalrelief = e + adjustablesurcharge - (o - 6e6), s = adjustablesurcharge - marginalrelief) : s = adjustablesurcharge, s = Math.round(s, 0)) : o > 1e8 && (adjustablesurcharge = e * .05, e + adjustablesurcharge > o - 592e5 ? (marginalrelief = e + adjustablesurcharge - (o - 592e5), s = adjustablesurcharge - marginalrelief) : s = adjustablesurcharge, s = Math.round(s, 0)), h = (e + s) * .02, c = (e + s) * .01) : l == "Partnership Firms" || l == "Local Authority" || l == "Firms" ? (e = o * .3, e = Math.round(e, 0), o >= 1e7 && (adjustablesurcharge = e * .12, e + adjustablesurcharge > o - 7e6 ? (marginalrelief = e + adjustablesurcharge - (o - 7e6), s = adjustablesurcharge - marginalrelief) : s = adjustablesurcharge, s = Math.round(s, 0)), h = (e + s) * .02, c = (e + s) * .01) : l == "Co-operative Society" ? (o <= 1e4 ? e = o * .1 : o > 1e4 && o <= 2e4 ? e = (o - 1e4) * .2 + 1e3 : o > 2e4 && (e = (o - 2e4) * .3 + 3e3), e = Math.round(e, 0), o >= 1e7 && (adjustablesurcharge = e * .12, e + adjustablesurcharge > o - 7003e3 ? (marginalrelief = e + adjustablesurcharge - (o - 7003e3), s = adjustablesurcharge - marginalrelief) : s = adjustablesurcharge, s = Math.round(s, 0)), h = (e + s) * .02, c = (e + s) * .01) : l == "AOPs/BOI" && (o <= 25e4 ? e = 0 : o > 25e4 && o <= 5e5 ? e = (o - 25e4) * .1 : o > 5e5 && o <= 1e6 ? e = (o - 5e5) * .2 + 25e3 : o > 1e6 && (e = (o - 1e6) * .3 + 125e3), e = Math.round(e, 0), o >= 1e7 && (adjustablesurcharge = e * .15, e + adjustablesurcharge > o - 7175e3 ? (marginalrelief = e + adjustablesurcharge - (o - 7175e3), s = adjustablesurcharge - marginalrelief) : s = adjustablesurcharge, s = Math.round(s, 0)), h = (e + s) * .02, c = (e + s) * .01);
    h = Math.round(h, 0);
    c = Math.round(c, 0);
    y = e + s + h + c;
    p = y / o * 100;
    i.value = getNum(p).toFixed(2)
}

function calculateDataDF201718Previous(n, t, i, r) {
    var h = n.value,
        u = 0,
        e = 0,
        o = 0,
        s = 0,
        c = 0,
        f = parseInt(getNum(t.value)),
        l;
    h == "Domestic Company" ? (u = f * .3, u = Math.round(u, 0), f > 1e7 && f <= 1e8 ? (adjustablesurcharge = u * .07, u + adjustablesurcharge > f - 7e6 ? (marginalrelief = u + adjustablesurcharge - (f - 7e6), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)) : f > 1e8 && (adjustablesurcharge = u * .12, u + adjustablesurcharge > f - 679e5 ? (marginalrelief = u + adjustablesurcharge - (f - 679e5), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "Foreign Company" ? (u = f * .4, u = Math.round(u, 0), f > 1e7 && f <= 1e8 ? (adjustablesurcharge = u * .02, u + adjustablesurcharge > f - 6e6 ? (marginalrelief = u + adjustablesurcharge - (f - 6e6), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)) : f > 1e8 && (adjustablesurcharge = u * .05, u + adjustablesurcharge > f - 592e5 ? (marginalrelief = u + adjustablesurcharge - (f - 592e5), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "Partnership Firms" || h == "Local Authority" || h == "Firms" ? (u = f * .3, u = Math.round(u, 0), f >= 1e7 && (adjustablesurcharge = u * .12, u + adjustablesurcharge > f - 7e6 ? (marginalrelief = u + adjustablesurcharge - (f - 7e6), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "Co-operative Society" ? (f <= 1e4 ? u = f * .1 : f > 1e4 && f <= 2e4 ? u = (f - 1e4) * .2 + 1e3 : f > 2e4 && (u = (f - 2e4) * .3 + 3e3), u = Math.round(u, 0), f >= 1e7 && (adjustablesurcharge = u * .12, u + adjustablesurcharge > f - 7003e3 ? (marginalrelief = u + adjustablesurcharge - (f - 7003e3), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "AOPs/BOI" && (f <= 25e4 ? u = 0 : f > 25e4 && f <= 5e5 ? u = (f - 25e4) * .1 : f > 5e5 && f <= 1e6 ? u = (f - 5e5) * .2 + 25e3 : f > 1e6 && (u = (f - 1e6) * .3 + 125e3), u = Math.round(u, 0), f >= 1e7 && (adjustablesurcharge = u * .12, u + adjustablesurcharge > f - 7175e3 ? (marginalrelief = u + adjustablesurcharge - (f - 7175e3), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01);
    o = Math.round(o, 0);
    s = Math.round(s, 0);
    c = u + e + o + s;
    l = c / f * 100;
    r.value = getNum(l).toFixed(2)
}

function calculateDataDF201617Current(n, t, i) {
    var s = n.value,
        r = 0,
        f = 0,
        e = 0,
        o = 0,
        h = 0,
        u = parseInt(getNum(t.value)),
        c;
    s == "Domestic Company" ? (r = u * .3, r = Math.round(r, 0), u > 1e7 && u <= 1e8 ? (adjustablesurcharge = r * .07, r + adjustablesurcharge > u - 7e6 ? (marginalrelief = r + adjustablesurcharge - (u - 7e6), f = adjustablesurcharge - marginalrelief) : f = adjustablesurcharge, f = Math.round(f, 0)) : u > 1e8 && (adjustablesurcharge = r * .12, r + adjustablesurcharge > u - 679e5 ? (marginalrelief = r + adjustablesurcharge - (u - 679e5), f = adjustablesurcharge - marginalrelief) : f = adjustablesurcharge, f = Math.round(f, 0)), e = (r + f) * .02, o = (r + f) * .01) : s == "Foreign Company" ? (r = u * .4, r = Math.round(r, 0), u > 1e7 && u <= 1e8 ? (adjustablesurcharge = r * .02, r + adjustablesurcharge > u - 6e6 ? (marginalrelief = r + adjustablesurcharge - (u - 6e6), f = adjustablesurcharge - marginalrelief) : f = adjustablesurcharge, f = Math.round(f, 0)) : u > 1e8 && (adjustablesurcharge = r * .05, r + adjustablesurcharge > u - 592e5 ? (marginalrelief = r + adjustablesurcharge - (u - 592e5), f = adjustablesurcharge - marginalrelief) : f = adjustablesurcharge, f = Math.round(f, 0)), e = (r + f) * .02, o = (r + f) * .01) : s == "Partnership Firms" || s == "Local Authority" || s == "Firms" ? (r = u * .3, r = Math.round(r, 0), u >= 1e7 && (adjustablesurcharge = r * .12, r + adjustablesurcharge > u - 7e6 ? (marginalrelief = r + adjustablesurcharge - (u - 7e6), f = adjustablesurcharge - marginalrelief) : f = adjustablesurcharge, f = Math.round(f, 0)), e = (r + f) * .02, o = (r + f) * .01) : s == "Co-operative Society" ? (u <= 1e4 ? r = u * .1 : u > 1e4 && u <= 2e4 ? r = (u - 1e4) * .2 + 1e3 : u > 2e4 && (r = (u - 2e4) * .3 + 3e3), r = Math.round(r, 0), u >= 1e7 && (adjustablesurcharge = r * .12, r + adjustablesurcharge > u - 7003e3 ? (marginalrelief = r + adjustablesurcharge - (u - 7003e3), f = adjustablesurcharge - marginalrelief) : f = adjustablesurcharge, f = Math.round(f, 0)), e = (r + f) * .02, o = (r + f) * .01) : s == "AOPs/BOI" && (u <= 25e4 ? r = 0 : u > 25e4 && u <= 5e5 ? r = (u - 25e4) * .1 : u > 5e5 && u <= 1e6 ? r = (u - 5e5) * .2 + 25e3 : u > 1e6 && (r = (u - 1e6) * .3 + 125e3), r = Math.round(r, 0), u >= 1e7 && (adjustablesurcharge = r * .12, r + adjustablesurcharge > u - 7175e3 ? (marginalrelief = r + adjustablesurcharge - (u - 7175e3), f = adjustablesurcharge - marginalrelief) : f = adjustablesurcharge, f = Math.round(f, 0)), e = (r + f) * .02, o = (r + f) * .01);
    e = Math.round(e, 0);
    o = Math.round(o, 0);
    h = r + f + e + o;
    c = h / u * 100;
    i.value = getNum(c).toFixed(2)
}

function calculateDataDF201617Previous(n, t, i, r) {
    var h = n.value,
        u = 0,
        e = 0,
        o = 0,
        s = 0,
        c = 0,
        f = parseInt(getNum(t.value)),
        l;
    h == "Domestic Company" ? (u = f * .3, u = Math.round(u, 0), f > 1e7 && f <= 1e8 ? (adjustablesurcharge = u * .05, u + adjustablesurcharge > f - 7e6 ? (marginalrelief = u + adjustablesurcharge - (f - 7e6), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)) : f > 1e8 && (adjustablesurcharge = u * .1, u + adjustablesurcharge > f - 685e5 ? (marginalrelief = u + adjustablesurcharge - (f - 685e5), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "Foreign Company" ? (u = f * .4, u = Math.round(u, 0), f > 1e7 && f <= 1e8 ? (adjustablesurcharge = u * .02, u + adjustablesurcharge > f - 6e6 ? (marginalrelief = u + adjustablesurcharge - (f - 6e6), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)) : f > 1e8 && (adjustablesurcharge = u * .05, u + adjustablesurcharge > f - 592e5 ? (marginalrelief = u + adjustablesurcharge - (f - 592e5), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "Partnership Firms" || h == "Local Authority" || h == "Firms" ? (u = f * .3, u = Math.round(u, 0), f >= 1e7 && (adjustablesurcharge = u * .1, u + adjustablesurcharge > f - 7e6 ? (marginalrelief = u + adjustablesurcharge - (f - 7e6), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "Co-operative Society" ? (f <= 1e4 ? u = f * .1 : f > 1e4 && f <= 2e4 ? u = (f - 1e4) * .2 + 1e3 : f > 2e4 && (u = (f - 2e4) * .3 + 3e3), u = Math.round(u, 0), f >= 1e7 && (adjustablesurcharge = u * .1, u + adjustablesurcharge > f - 7003e3 ? (marginalrelief = u + adjustablesurcharge - (f - 7003e3), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "AOPs/BOI" && (f <= 25e4 ? u = 0 : f > 25e4 && f <= 5e5 ? u = (f - 25e4) * .1 : f > 5e5 && f <= 1e6 ? u = (f - 5e5) * .2 + 25e3 : f > 1e6 && (u = (f - 1e6) * .3 + 125e3), u = Math.round(u, 0), f >= 1e7 && (adjustablesurcharge = u * .1, u + adjustablesurcharge > f - 7175e3 ? (marginalrelief = u + adjustablesurcharge - (f - 7175e3), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01);
    o = Math.round(o, 0);
    s = Math.round(s, 0);
    c = u + e + o + s;
    l = c / f * 100;
    r.value = getNum(l).toFixed(2)
}

function calculateDataDF201516(n, t, i, r) {
    var h = n.value,
        u = 0,
        e = 0,
        o = 0,
        s = 0,
        l = 0,
        f = parseInt(getNum(t.value)),
        c;
    h == "Domestic Company" ? (u = f * .3, u = Math.round(u, 0), f > 1e7 && f <= 1e8 ? (adjustablesurcharge = u * .05, u + adjustablesurcharge > f - 7e6 ? (marginalrelief = u + adjustablesurcharge - (f - 7e6), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)) : f > 1e8 && (adjustablesurcharge = u * .1, u + adjustablesurcharge > f - 685e5 ? (marginalrelief = u + adjustablesurcharge - (f - 685e5), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "Foreign Company" ? (u = f * .4, u = Math.round(u, 0), f > 1e7 && f <= 1e8 ? (adjustablesurcharge = u * .02, u + adjustablesurcharge > f - 6e6 ? (marginalrelief = u + adjustablesurcharge - (f - 6e6), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)) : f > 1e8 && (adjustablesurcharge = u * .05, u + adjustablesurcharge > f - 592e5 ? (marginalrelief = u + adjustablesurcharge - (f - 592e5), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "Partnership Firms" || h == "Local Authority" || h == "Firms" ? (u = f * .3, u = Math.round(u, 0), f >= 1e7 && (adjustablesurcharge = u * .1, u + adjustablesurcharge > f - 7e6 ? (marginalrelief = u + adjustablesurcharge - (f - 7e6), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "Co-operative Society" ? (f <= 1e4 ? u = f * .1 : f > 1e4 && f <= 2e4 ? u = (f - 1e4) * .2 + 1e3 : f > 2e4 && (u = (f - 2e4) * .3 + 3e3), u = Math.round(u, 0), f >= 1e7 && (adjustablesurcharge = u * .1, u + adjustablesurcharge > f - 7003e3 ? (marginalrelief = u + adjustablesurcharge - (f - 7003e3), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "AOPs/BOI" && (f <= 25e4 ? u = 0 : f > 25e4 && f <= 5e5 ? u = (f - 25e4) * .1 : f > 5e5 && f <= 1e6 ? u = (f - 5e5) * .2 + 25e3 : f > 1e6 && (u = (f - 1e6) * .3 + 125e3), u = Math.round(u, 0), f >= 1e7 && (adjustablesurcharge = u * .1, u + adjustablesurcharge > f - 7175e3 ? (marginalrelief = u + adjustablesurcharge - (f - 7175e3), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01);
    o = Math.round(o, 0);
    s = Math.round(s, 0);
    l = u + e + o + s;
    c = l / f * 100;
    i.value = getNum(c).toFixed(2);
    r.value = getNum(c).toFixed(2)
}

function calculateDataDF201415(n, t, i, r) {
    var h = n.value,
        u = 0,
        e = 0,
        o = 0,
        s = 0,
        l = 0,
        f = parseInt(getNum(t.value)),
        c;
    h == "Domestic Company" ? (u = f * .3, u = Math.round(u, 0), f > 1e7 && f <= 1e8 ? (adjustablesurcharge = u * .05, u + adjustablesurcharge > f - 7e6 ? (marginalrelief = u + adjustablesurcharge - (f - 7e6), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)) : f > 1e8 && (adjustablesurcharge = u * .1, u + adjustablesurcharge > f - 685e5 ? (marginalrelief = u + adjustablesurcharge - (f - 685e5), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "Foreign Company" ? (u = f * .4, u = Math.round(u, 0), f > 1e7 && f <= 1e8 ? (adjustablesurcharge = u * .02, u + adjustablesurcharge > f - 6e6 ? (marginalrelief = u + adjustablesurcharge - (f - 6e6), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)) : f > 1e8 && (adjustablesurcharge = u * .05, u + adjustablesurcharge > f - 592e5 ? (marginalrelief = u + adjustablesurcharge - (f - 592e5), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "Partnership Firms" || h == "Local Authority" || h == "Firms" ? (u = f * .3, u = Math.round(u, 0), f >= 1e7 && (adjustablesurcharge = u * .1, u + adjustablesurcharge > f - 7e6 ? (marginalrelief = u + adjustablesurcharge - (f - 7e6), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "Co-operative Society" ? (f <= 1e4 ? u = f * .1 : f > 1e4 && f <= 2e4 ? u = (f - 1e4) * .2 + 1e3 : f > 2e4 && (u = (f - 2e4) * .3 + 3e3), u = Math.round(u, 0), f >= 1e7 && (adjustablesurcharge = u * .1, u + adjustablesurcharge > f - 7003e3 ? (marginalrelief = u + adjustablesurcharge - (f - 7003e3), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "AOPs/BOI" && (f <= 2e5 ? u = 0 : f > 2e5 && f <= 5e5 ? u = (f - 2e5) * .1 : f > 5e5 && f <= 1e6 ? u = (f - 5e5) * .2 + 3e4 : f > 1e6 && (u = (f - 1e6) * .3 + 13e4), u = Math.round(u, 0), f >= 1e7 && (adjustablesurcharge = u * .1, u + adjustablesurcharge > f - 717e4 ? (marginalrelief = u + adjustablesurcharge - (f - 717e4), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01);
    o = Math.round(o, 0);
    s = Math.round(s, 0);
    l = u + e + o + s;
    c = l / f * 100;
    i.value = getNum(c).toFixed(2);
    r.value = getNum(c).toFixed(2)
}

function calculateDataDF201314(n, t, i, r) {
    var h = n.value,
        u = 0,
        e = 0,
        o = 0,
        s = 0,
        f = parseInt(getNum(t.value)),
        l = 0,
        c;
    h == "Domestic Company" ? (u = f * .3, u = Math.round(u, 0), f >= 1e7 && (adjustablesurcharge = u * .05, u + adjustablesurcharge > f - 7e6 ? (marginalrelief = u + adjustablesurcharge - (f - 7e6), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "Foreign Company" ? (u = f * .4, u = Math.round(u, 0), f >= 1e7 && (adjustablesurcharge = u * .02, u + adjustablesurcharge > f - 6e6 ? (marginalrelief = u + adjustablesurcharge - (f - 6e6), e = adjustablesurcharge - marginalrelief) : e = adjustablesurcharge, e = Math.round(e, 0)), o = (u + e) * .02, s = (u + e) * .01) : h == "Partnership Firms" || h == "Local Authority" || h == "Firms" ? (u = f * .3, u = Math.round(u, 0), e = 0, o = (u + e) * .02, s = (u + e) * .01) : h == "Co-operative Society" ? (f <= 1e4 ? u = f * .1 : f > 1e4 && f <= 2e4 ? u = (f - 1e4) * .2 + 1e3 : f > 2e4 && (u = (f - 2e4) * .3 + 3e3), u = Math.round(u, 0), e = 0, o = (u + e) * .02, s = (u + e) * .01) : h == "AOPs/BOI" && (f <= 2e5 ? u = 0 : f > 2e5 && f <= 5e5 ? u = (f - 2e5) * .1 : f > 5e5 && f <= 1e6 ? u = (f - 5e5) * .2 + 3e4 : f > 1e6 && (u = (f - 1e6) * .3 + 13e4), u = Math.round(u, 0), e = 0, o = (u + e) * .02, s = (u + e) * .01);
    o = Math.round(o, 0);
    s = Math.round(s, 0);
    l = u + e + o + s;
    c = l / f * 100;
    i.value = getNum(c).toFixed(2);
    r.value = getNum(c).toFixed(2)
}
var monthNames, dateParsePatterns;
Calendar = function() {
    function n(t) {
        var f, i, e;
        t = t || {};
        this.args = t = si(t, {
            animation: !et,
            cont: null,
            bottomBar: !0,
            date: !0,
            fdow: u("fdow"),
            min: null,
            max: null,
            reverseWheel: !1,
            selection: [],
            selectionType: n.SEL_SINGLE,
            weekNumbers: !1,
            align: "Bl/ / /T/r",
            inputField: null,
            trigger: null,
            dateFormat: "%Y-%m-%d",
            opacity: r ? 1 : 3,
            titleFormat: "%b %Y",
            showTime: !1,
            timePos: "right",
            time: !0,
            minuteStep: 5,
            disabled: l,
            checkRange: !1,
            dateInfo: l,
            onChange: l,
            onSelect: l,
            onTimeChange: l,
            onFocus: l,
            onBlur: l
        });
        this.handlers = {};
        f = this;
        i = new Date;
        t.min = nt(t.min);
        t.max = nt(t.max);
        t.date === !0 && (t.date = i);
        t.time === !0 && (t.time = i.getHours() * 100 + Math.floor(i.getMinutes() / t.minuteStep) * t.minuteStep);
        this.date = nt(t.date);
        this.time = t.time;
        this.fdow = t.fdow;
        li("onChange onSelect onTimeChange onFocus onBlur".split(/\s+/), function(n) {
            var i = t[n];
            i instanceof Array || (i = [i]);
            f.handlers[n] = i
        });
        this.selection = new n.Selection(t.selection, t.selectionType, gi, this);
        e = wi.call(this);
        t.cont && ft(t.cont).appendChild(e);
        t.trigger && this.manageFields(t.trigger, t.inputField, t.dateFormat)
    }

    function wt(n) {
        var t = ["<table", s, "><tr>"],
            r = 0,
            i;
        for (n.args.weekNumbers && t.push("<td><div class='DynarchCalendar-weekNumber'>", u("wk"), "<\/div><\/td>"); r < 7;) i = (r++ + n.fdow) % 7, t.push("<td><div", u("weekend").indexOf(i) >= 0 ? " class='DynarchCalendar-weekend'>" : ">", u("sdn")[i], "<\/div><\/td>");
        return t.push("<\/tr><\/table>"), t.join("")
    }

    function ot(n, t, i) {
        var c, e, o;
        t = t || n.date;
        i = i || n.fdow;
        t = new Date(t.getFullYear(), t.getMonth(), t.getDate(), 12, 0, 0, 0);
        var k = t.getMonth(),
            r = [],
            f = 0,
            w = n.args.weekNumbers;
        t.setDate(1);
        c = (t.getDay() - i) % 7;
        c < 0 && (c += 7);
        t.setDate(-c);
        t.setDate(t.getDate() + 1);
        var y = new Date,
            d = y.getDate(),
            g = y.getMonth(),
            nt = y.getFullYear();
        for (r[f++] = "<table class='DynarchCalendar-bodyTable'" + s + ">", e = 0; e < 6; ++e) {
            for (r[f++] = "<tr class='DynarchCalendar-week", e == 0 && (r[f++] = " DynarchCalendar-first-row"), e == 5 && (r[f++] = " DynarchCalendar-last-row"), r[f++] = "'>", w && (r[f++] = "<td class='DynarchCalendar-first-col'><div class='DynarchCalendar-weekNumber'>" + oi(t) + "<\/div><\/td>"), o = 0; o < 7; ++o) {
                var l = t.getDate(),
                    a = t.getMonth(),
                    p = t.getFullYear(),
                    v = 1e4 * p + 100 * (a + 1) + l,
                    b = n.selection.isSelected(v),
                    h = n.isDisabled(t);
                r[f++] = "<td class='";
                o != 0 || w || (r[f++] = " DynarchCalendar-first-col");
                o == 0 && e == 0 && (n._firstDateVisible = v);
                o == 6 && (r[f++] = " DynarchCalendar-last-col", e == 5 && (n._lastDateVisible = v));
                b && (r[f++] = " DynarchCalendar-td-selected");
                r[f++] = "'><div dyc-type='date' unselectable='on' dyc-date='" + v + "' ";
                h && (r[f++] = "disabled='1' ");
                r[f++] = "class='DynarchCalendar-day";
                u("weekend").indexOf(t.getDay()) >= 0 && (r[f++] = " DynarchCalendar-weekend");
                a != k && (r[f++] = " DynarchCalendar-day-othermonth");
                l == d && a == g && p == nt && (r[f++] = " DynarchCalendar-day-today");
                h && (r[f++] = " DynarchCalendar-day-disabled");
                b && (r[f++] = " DynarchCalendar-day-selected");
                h = n.args.dateInfo(t);
                h && h.klass && (r[f++] = " " + h.klass);
                r[f++] = "'>" + l + "<\/div><\/td>";
                t = new Date(p, a, l + 1, 12, 0, 0, 0)
            }
            r[f++] = "<\/tr>"
        }
        return r[f++] = "<\/table>", r.join("")
    }

    function yi(n) {
        var t = ["<table class='DynarchCalendar-topCont'", s, "><tr><td><div class='DynarchCalendar'>", r ? "<a class='DynarchCalendar-focusLink' href='#'><\/a>" : "<button class='DynarchCalendar-focusLink'><\/button>", "<div class='DynarchCalendar-topBar'><div dyc-type='nav' dyc-btn='-Y' dyc-cls='hover-navBtn,pressed-navBtn' class='DynarchCalendar-navBtn DynarchCalendar-prevYear'><div><\/div><\/div><div dyc-type='nav' dyc-btn='+Y' dyc-cls='hover-navBtn,pressed-navBtn' class='DynarchCalendar-navBtn DynarchCalendar-nextYear'><div><\/div><\/div><div dyc-type='nav' dyc-btn='-M' dyc-cls='hover-navBtn,pressed-navBtn' class='DynarchCalendar-navBtn DynarchCalendar-prevMonth'><div><\/div><\/div><div dyc-type='nav' dyc-btn='+M' dyc-cls='hover-navBtn,pressed-navBtn' class='DynarchCalendar-navBtn DynarchCalendar-nextMonth'><div><\/div><\/div><table class='DynarchCalendar-titleCont'", s, "><tr><td><div dyc-type='title' dyc-btn='menu' dyc-cls='hover-title,pressed-title' class='DynarchCalendar-title'>", bt(n), "<\/div><\/td><\/tr><\/table><div class='DynarchCalendar-dayNames'>", wt(n), "<\/div><\/div><div class='DynarchCalendar-body'><\/div>"];
        return (n.args.bottomBar || n.args.showTime) && t.push("<div class='DynarchCalendar-bottomBar'>", dt(n), "<\/div>"), t.push("<div class='DynarchCalendar-menu' style='display: none'>", kt(n), "<\/div><div class='DynarchCalendar-tooltip'><\/div><\/div><\/td><\/tr><\/table>"), t.join("")
    }

    function bt(n) {
        return "<div unselectable='on'>" + y(n.date, n.args.titleFormat) + "<\/div>"
    }

    function kt(n) {
        for (var t = ["<table height='100%'", s, "><tr><td><table style='margin-top: 1.5em'", s, "><tr><td colspan='3'><input dyc-btn='year' class='DynarchCalendar-menu-year' size='6' value='", n.date.getFullYear(), "' /><\/td><\/tr><tr><td><div dyc-type='menubtn' dyc-cls='hover-navBtn,pressed-navBtn' dyc-btn='today'>", u("goToday"), "<\/div><\/td><\/tr><\/table><p class='DynarchCalendar-menu-sep'>&nbsp;<\/p><table class='DynarchCalendar-menu-mtable'", s, ">"], e = u("smn"), r = 0, i = t.length, f; r < 12;) {
            for (t[i++] = "<tr>", f = 4; --f > 0;) t[i++] = "<td><div dyc-type='menubtn' dyc-cls='hover-navBtn,pressed-navBtn' dyc-btn='m" + r + "' class='DynarchCalendar-menu-month'>" + e[r++] + "<\/div><\/td>";
            t[i++] = "<\/tr>"
        }
        return t[i++] = "<\/table><\/td><\/tr><\/table>", t.join("")
    }

    function pi(n, t) {
        t.push("<table class='DynarchCalendar-time'" + s + "><tr><td rowspan='2'><div dyc-type='time-hour' dyc-cls='hover-time,pressed-time' class='DynarchCalendar-time-hour'><\/div><\/td><td dyc-type='time-hour+' dyc-cls='hover-time,pressed-time' class='DynarchCalendar-time-up'><\/td><td rowspan='2' class='DynarchCalendar-time-sep'><\/td><td rowspan='2'><div dyc-type='time-min' dyc-cls='hover-time,pressed-time' class='DynarchCalendar-time-minute'><\/div><\/td><td dyc-type='time-min+' dyc-cls='hover-time,pressed-time' class='DynarchCalendar-time-up'><\/td>");
        n.args.showTime == 12 && t.push("<td rowspan='2' class='DynarchCalendar-time-sep'><\/td><td rowspan='2'><div class='DynarchCalendar-time-am' dyc-type='time-am' dyc-cls='hover-time,pressed-time'><\/div><\/td>");
        t.push("<\/tr><tr><td dyc-type='time-hour-' dyc-cls='hover-time,pressed-time' class='DynarchCalendar-time-down'><\/td><td dyc-type='time-min-' dyc-cls='hover-time,pressed-time' class='DynarchCalendar-time-down'><\/td><\/tr><\/table>")
    }

    function dt(n) {
        function r() {
            i.showTime && (t.push("<td>"), pi(n, t), t.push("<\/td>"))
        }
        var t = [],
            i = n.args;
        return t.push("<table", s, " style='width:100%'><tr>"), i.timePos == "left" && r(), i.bottomBar && (t.push("<td>"), t.push("<table", s, "><tr><td><div dyc-btn='today' dyc-cls='hover-bottomBar-today,pressed-bottomBar-today' dyc-type='bottomBar-today' class='DynarchCalendar-bottomBar-today'>", u("today"), "<\/div><\/td><\/tr><\/table>"), t.push("<\/td>")), i.timePos == "right" && r(), t.push("<\/tr><\/table>"), t.join("")
    }

    function wi() {
        var u = hi("div"),
            t = this.els = {},
            n = {
                mousedown: i(ri, this, !0),
                mouseup: i(ri, this, !1),
                mouseover: i(lt, this, !0),
                mouseout: i(lt, this, !1),
                keypress: i(nr, this)
            };
        return n[vi ? "DOMMouseScroll" : "mousewheel"] = i(di, this), r && (n.dblclick = n.mousedown, n.keydown = n.keypress), u.innerHTML = yi(this), it(u.firstChild, function(n) {
            var i = st[n.className];
            i && (t[i] = n);
            r && n.setAttribute("unselectable", "on")
        }), h(t.main, n), h([t.focusLink, t.yearInput], this._focusEvents = {
            focus: i(gt, this),
            blur: i(bi, this)
        }), this.moveTo(this.date, !1), this.setTime(null, !0), t.topCont
    }

    function gt() {
        this._bluringTimeout && clearTimeout(this._bluringTimeout);
        this.focused = !0;
        vt(this.els.main, "DynarchCalendar-focused");
        this.callHooks("onFocus", this)
    }

    function ni() {
        this.focused = !1;
        b(this.els.main, "DynarchCalendar-focused");
        this._menuVisible && v(this, !1);
        this.args.cont || this.hide();
        this.callHooks("onBlur", this)
    }

    function bi() {
        this._bluringTimeout = setTimeout(i(ni, this), 50)
    }

    function ti(n) {
        switch (n) {
            case "time-hour+":
                this.setHours(this.getHours() + 1);
                break;
            case "time-hour-":
                this.setHours(this.getHours() - 1);
                break;
            case "time-min+":
                this.setMinutes(this.getMinutes() + this.args.minuteStep);
                break;
            case "time-min-":
                this.setMinutes(this.getMinutes() - this.args.minuteStep);
                break;
            default:
                return
        }
    }

    function d(n, t, i) {
        this._bodyAnim && this._bodyAnim.stop();
        var r;
        if (t != 0) {
            r = new Date(n.date);
            r.setDate(1);
            switch (t) {
                case "-Y":
                case -2:
                    r.setFullYear(r.getFullYear() - 1);
                    break;
                case "+Y":
                case 2:
                    r.setFullYear(r.getFullYear() + 1);
                    break;
                case "-M":
                case -1:
                    r.setMonth(r.getMonth() - 1);
                    break;
                case "+M":
                case 1:
                    r.setMonth(r.getMonth() + 1)
            }
        } else r = new Date;
        return n.moveTo(r, !i)
    }

    function v(n, t) {
        var i, r;
        n._menuVisible = t;
        p(t, n.els.title, "DynarchCalendar-pressed-title");
        i = n.els.menu;
        et && (i.style.height = n.els.main.offsetHeight + "px");
        n.args.animation ? (n._menuAnim && n._menuAnim.stop(), r = n.els.main.offsetHeight, et && (i.style.width = n.els.topBar.offsetWidth + "px"), t && (i.firstChild.style.marginTop = -r + "px", n.args.opacity > 0 && c(i, 0), yt(i, !0)), n._menuAnim = rt({
            onUpdate: function(u, f) {
                i.firstChild.style.marginTop = f(a.accel_b(u), -r, 0, !t) + "px";
                n.args.opacity > 0 && c(i, f(a.accel_b(u), 0, .85, !t))
            },
            onStop: function() {
                n.args.opacity > 0 && c(i, .85);
                i.firstChild.style.marginTop = "";
                n._menuAnim = null;
                t || (yt(i, !1), n.focused && n.focus())
            }
        })) : (yt(i, t), n.focused && n.focus())
    }

    function ri(t, u) {
        var o, k, y, s;
        if (u = u || window.event, o = ht(u), o && !o.getAttribute("disabled")) {
            var a = o.getAttribute("dyc-btn"),
                p = o.getAttribute("dyc-type"),
                s = o.getAttribute("dyc-date"),
                l = this.selection,
                w, c = {
                    mouseover: e,
                    mousemove: e,
                    mouseup: function() {
                        var n = o.getAttribute("dyc-cls");
                        n && b(o, ct(n, 1));
                        clearTimeout(w);
                        at(document, c, !0);
                        c = null
                    }
                };
            t ? (setTimeout(i(this.focus, this), 1), k = o.getAttribute("dyc-cls"), k && vt(o, ct(k, 1)), "menu" == a ? this.toggleMenu() : o && /^[+-][MY]$/.test(a) ? d(this, a) ? (y = i(function() {
                d(this, a, !0) ? w = setTimeout(y, 40) : (c.mouseup(), d(this, a))
            }, this), w = setTimeout(y, 350), h(document, c, !0)) : c.mouseup() : "year" == a ? (this.els.yearInput.focus(), this.els.yearInput.select()) : p == "time-am" ? h(document, c, !0) : /^time/.test(p) ? (y = i(function(n) {
                ti.call(this, n);
                w = setTimeout(y, 100)
            }, this, p), ti.call(this, p), w = setTimeout(y, 350), h(document, c, !0)) : (s && l.type && (l.type == n.SEL_MULTIPLE ? u.shiftKey && this._selRangeStart ? l.selectRange(this._selRangeStart, s) : (u.ctrlKey || l.isSelected(s) || l.clear(!0), l.set(s, !0), this._selRangeStart = s) : (l.set(s), this.moveTo(f(s), 2)), o = this._getDateDiv(s), lt.call(this, !0, {
                target: o
            })), h(document, c, !0)), r && c && /dbl/i.test(u.type) && c.mouseup(), /^(DynarchCalendar-(topBar|bottomBar|weekend|weekNumber|menu(-sep)?))?$/.test(o.className) && !this.args.cont && (c.mousemove = i(ki, this), this._mouseDiff = ci(u, ut(this.els.topCont)), h(document, c, !0))) : "today" == a ? (this._menuVisible || l.type != n.SEL_SINGLE || l.set(new Date), this.moveTo(new Date, !0), v(this, !1)) : /^m([0-9]+)/.test(a) ? (s = new Date(this.date), s.setDate(1), s.setMonth(RegExp.$1), s.setFullYear(this._getInputYear()), this.moveTo(s, !0), v(this, !1)) : p == "time-am" && this.setHours(this.getHours() + 12);
            r || e(u)
        }
    }

    function ki(n) {
        n = n || window.event;
        var t = this.els.topCont.style,
            i = ci(n, this._mouseDiff);
        t.left = i.x + "px";
        t.top = i.y + "px"
    }

    function ht(n) {
        for (var t = n.target || n.srcElement, i = t; t && t.getAttribute && !t.getAttribute("dyc-type");) t = t.parentNode;
        return t.getAttribute && t || i
    }

    function ct(n, t) {
        return "DynarchCalendar-" + n.split(/,/)[t]
    }

    function lt(n, t) {
        var i, r, u;
        t = t || window.event;
        i = ht(t);
        i && (r = i.getAttribute("dyc-type"), r && !i.getAttribute("disabled") && (n && this._bodyAnim && r == "date" || (u = i.getAttribute("dyc-cls"), u = u ? ct(u, 0) : "DynarchCalendar-hover-" + r, (r != "date" || this.selection.type) && p(n, i, u), r == "date" && (p(n, i.parentNode.parentNode, "DynarchCalendar-hover-week"), this._showTooltip(i.getAttribute("dyc-date"))), /^time-hour/.test(r) && p(n, this.els.timeHour, "DynarchCalendar-hover-time"), /^time-min/.test(r) && p(n, this.els.timeMinute, "DynarchCalendar-hover-time"), b(this._getDateDiv(this._lastHoverDate), "DynarchCalendar-hover-date"), this._lastHoverDate = null)));
        n || this._showTooltip()
    }

    function di(n) {
        var i;
        if (n = n || window.event, i = ht(n), i) {
            var r = i.getAttribute("dyc-btn"),
                u = i.getAttribute("dyc-type"),
                t = n.wheelDelta ? n.wheelDelta / 120 : -n.detail / 3;
            if (t = t < 0 ? -1 : t > 0 ? 1 : 0, this.args.reverseWheel && (t = -t), /^(time-(hour|min))/.test(u)) {
                switch (RegExp.$1) {
                    case "time-hour":
                        this.setHours(this.getHours() + t);
                        break;
                    case "time-min":
                        this.setMinutes(this.getMinutes() + this.args.minuteStep * t)
                }
                e(n)
            } else /Y/i.test(r) && (t *= 2), d(this, -t), e(n)
        }
    }

    function gi() {
        var n, t, i;
        this.refresh();
        n = this.inputField;
        t = this.selection;
        n && (i = t.print(this.dateFormat), /input|textarea/i.test(n.tagName) ? n.value = i : n.innerHTML = i);
        this.callHooks("onSelect", this, t)
    }

    function nr(t) {
        var l, s, i, w, a;
        if (!this._menuAnim) {
            t = t || window.event;
            var k = t.target || t.srcElement,
                g = k.getAttribute("dyc-btn"),
                r = t.keyCode,
                h = t.charCode || r,
                c = ui[r];
            if ("year" == g && r == 13) return i = new Date(this.date), i.setDate(1), i.setFullYear(this._getInputYear()), this.moveTo(i, !0), v(this, !1), e(t);
            if (this._menuVisible) {
                if (r == 27) return v(this, !1), e(t)
            } else {
                if (t.ctrlKey || (c = null), c != null || t.ctrlKey || (c = fi[r]), r == 36 && (c = 0), c != null) return d(this, c), e(t);
                if (h = String.fromCharCode(h).toLowerCase(), l = this.els.yearInput, s = this.selection, h == " ") return v(this, !0), this.focus(), l.focus(), l.select(), e(t);
                if (h >= "0" && h <= "9") return v(this, !0), this.focus(), l.value = h, l.focus(), e(t);
                for (var nt = u("mn"), a = t.shiftKey ? -1 : this.date.getMonth(), y = 0, p; ++y < 12;)
                    if (p = nt[(a + y) % 12].toLowerCase(), p.indexOf(h) == 0) return i = new Date(this.date), i.setDate(1), i.setMonth((a + y) % 12), this.moveTo(i, !0), e(t);
                if (r >= 37 && r <= 40) {
                    if (i = this._lastHoverDate, i || s.isEmpty() || (i = r < 39 ? s.getFirstDate() : s.getLastDate(), (i < this._firstDateVisible || i > this._lastDateVisible) && (i = null)), i) {
                        for (w = i, i = f(i), a = 100; a-- > 0;) {
                            switch (r) {
                                case 37:
                                    i.setDate(i.getDate() - 1);
                                    break;
                                case 38:
                                    i.setDate(i.getDate() - 7);
                                    break;
                                case 39:
                                    i.setDate(i.getDate() + 1);
                                    break;
                                case 40:
                                    i.setDate(i.getDate() + 7)
                            }
                            if (!this.isDisabled(i)) break
                        }
                        i = o(i);
                        (i < this._firstDateVisible || i > this._lastDateVisible) && this.moveTo(i)
                    } else i = r < 39 ? this._lastDateVisible : this._firstDateVisible;
                    return b(this._getDateDiv(w), vt(this._getDateDiv(i), "DynarchCalendar-hover-date")), this._lastHoverDate = i, e(t)
                }
                if (r == 13 && this._lastHoverDate) return s.type == n.SEL_MULTIPLE && (t.shiftKey || t.ctrlKey) ? (t.shiftKey && this._selRangeStart && (s.clear(!0), s.selectRange(this._selRangeStart, this._lastHoverDate)), t.ctrlKey && s.set(this._selRangeStart = this._lastHoverDate, !0)) : s.reset(this._selRangeStart = this._lastHoverDate), e(t);
                r != 27 || this.args.cont || this.hide()
            }
        }
    }

    function ei(n, t) {
        return n.replace(/\$\{([^:\}]+)(:[^\}]+)?\}/g, function(n, i, r) {
            var u = t[i],
                f;
            return r && (f = r.substr(1).split(/\s*\|\s*/), u = (u >= f.length ? f[f.length - 1] : f[u]).replace(/##?/g, function(n) {
                return n.length == 2 ? "#" : u
            })), u
        })
    }

    function u(n, t) {
        var i = k.__.data[n];
        return t && typeof i == "string" && (i = ei(i, t)), i
    }

    function oi(n) {
        var t, i;
        return n = new Date(n.getFullYear(), n.getMonth(), n.getDate(), 12, 0, 0), t = n.getDay(), n.setDate(n.getDate() - (t + 6) % 7 + 3), i = n.valueOf(), n.setMonth(0), n.setDate(4), Math.round((i - n.valueOf()) / 6048e5) + 1
    }

    function tr(n) {
        n = new Date(n.getFullYear(), n.getMonth(), n.getDate(), 12, 0, 0);
        var t = new Date(n.getFullYear(), 0, 1, 12, 0, 0),
            i = n - t;
        return Math.floor(i / 864e5)
    }

    function o(n) {
        return n instanceof Date ? 1e4 * n.getFullYear() + 100 * (n.getMonth() + 1) + n.getDate() : typeof n == "string" ? parseInt(n, 10) : n
    }

    function f(n, t, i, r, u) {
        var f, e;
        return n instanceof Date || (n = parseInt(n, 10), f = Math.floor(n / 1e4), n = n % 1e4, e = Math.floor(n / 100), n = n % 100, n = new Date(f, e - 1, n, t || 12, i || 0, r || 0, u || 0)), n
    }

    function g(n, t, i) {
        var r = n.getFullYear(),
            u = n.getMonth(),
            f = n.getDate(),
            e = t.getFullYear(),
            o = t.getMonth(),
            s = t.getDate();
        return r < e ? -3 : r > e ? 3 : u < o ? -2 : u > o ? 2 : i ? 0 : f < s ? -1 : f > s ? 1 : 0
    }

    function y(n, t) {
        var f = n.getMonth(),
            s = n.getDate(),
            c = n.getFullYear(),
            i = oi(n),
            h = n.getDay(),
            r = n.getHours(),
            l = r >= 12,
            e = l ? r - 12 : r,
            o = tr(n),
            a = n.getMinutes(),
            v = n.getSeconds(),
            y;
        return e === 0 && (e = 12), y = {
            "%a": u("sdn")[h],
            "%A": u("dn")[h],
            "%b": u("smn")[f],
            "%B": u("mn")[f],
            "%C": 1 + Math.floor(c / 100),
            "%d": s < 10 ? "0" + s : s,
            "%e": s,
            "%H": r < 10 ? "0" + r : r,
            "%I": e < 10 ? "0" + e : e,
            "%j": o < 10 ? "00" + o : o < 100 ? "0" + o : o,
            "%k": r,
            "%l": e,
            "%m": f < 9 ? "0" + (1 + f) : 1 + f,
            "%o": 1 + f,
            "%M": a < 10 ? "0" + a : a,
            "%n": "\n",
            "%p": l ? "PM" : "AM",
            "%P": l ? "pm" : "am",
            "%s": Math.floor(n.getTime() / 1e3),
            "%S": v < 10 ? "0" + v : v,
            "%t": "\t",
            "%U": i < 10 ? "0" + i : i,
            "%W": i < 10 ? "0" + i : i,
            "%V": i < 10 ? "0" + i : i,
            "%u": h + 1,
            "%w": h,
            "%y": ("" + c).substr(2, 2),
            "%Y": c,
            "%%": "%"
        }, t.replace(/%./g, function(n) {
            return y.hasOwnProperty(n) ? y[n] : n
        })
    }

    function nt(n) {
        if (n) {
            if (typeof n == "number") return f(n);
            if (!(n instanceof Date)) {
                var t = n.split(/-/);
                return new Date(parseInt(t[0], 10), parseInt(t[1], 10) - 1, parseInt(t[2], 10), 12, 0, 0, 0)
            }
        }
        return n
    }

    function ir(n) {
        if (/\S/.test(n)) {
            n = n.toLowerCase();

            function i(t) {
                for (var i = t.length; --i >= 0;)
                    if (t[i].toLowerCase().indexOf(n) == 0) return i
            }
            var t = i(u("smn")) || i(u("mn"));
            return t != null && t++, t
        }
    }

    function si(n, t, i, r) {
        r = {};
        for (i in t) t.hasOwnProperty(i) && (r[i] = t[i]);
        for (i in n) n.hasOwnProperty(i) && (r[i] = n[i]);
        return r
    }

    function h(n, t, i, u) {
        var f;
        if (n instanceof Array)
            for (f = n.length; --f >= 0;) h(n[f], t, i, u);
        else if (typeof t == "object")
            for (f in t) t.hasOwnProperty(f) && h(n, f, t[f], i);
        else n.addEventListener ? n.addEventListener(t, i, r ? !0 : !!u) : n.attachEvent ? n.attachEvent("on" + t, i) : n["on" + t] = i
    }

    function at(n, t, i, u) {
        var f;
        if (n instanceof Array)
            for (f = n.length; --f >= 0;) at(n[f], t, i);
        else if (typeof t == "object")
            for (f in t) t.hasOwnProperty(f) && at(n, f, t[f], i);
        else n.removeEventListener ? n.removeEventListener(t, i, r ? !0 : !!u) : n.detachEvent ? n.detachEvent("on" + t, i) : n["on" + t] = null
    }

    function e(n) {
        return n = n || window.event, r ? (n.cancelBubble = !0, n.returnValue = !1) : (n.preventDefault(), n.stopPropagation()), !1
    }

    function b(n, t, i) {
        if (n) {
            for (var u = n.className.replace(/^\s+|\s+$/, "").split(/\x20/), f = [], r = u.length; r > 0;) u[--r] != t && f.push(u[r]);
            i && f.push(i);
            n.className = f.join(" ")
        }
        return i
    }

    function vt(n, t) {
        return b(n, t, t)
    }

    function p(n, t, i) {
        if (t instanceof Array)
            for (var r = t.length; --r >= 0;) p(n, t[r], i);
        else b(t, i, n ? i : null);
        return n
    }

    function hi(n, t, i) {
        var r = null;
        return r = document.createElementNS ? document.createElementNS("http://www.w3.org/1999/xhtml", n) : document.createElement(n), t && (r.className = t), i && i.appendChild(r), r
    }

    function tt(n, t) {
        t == null && (t = 0);
        var i, r, u;
        try {
            i = Array.prototype.slice.call(n, t)
        } catch (f) {
            for (i = new Array(n.length - t), r = t, u = 0; r < n.length; ++r, ++u) i[u] = n[r]
        }
        return i
    }

    function i(n, t) {
        var i = tt(arguments, 2);
        return t == undefined ? function() {
            return n.apply(this, i.concat(tt(arguments)))
        } : function() {
            return n.apply(t, i.concat(tt(arguments)))
        }
    }

    function it(n, t) {
        if (!t(n))
            for (var i = n.firstChild; i; i = i.nextSibling) i.nodeType == 1 && it(i, t)
    }

    function rt(n, t, i) {
        function u(n, t, i, r) {
            return r ? i + n * (t - i) : t + n * (i - t)
        }

        function e() {
            t && f();
            i = 0;
            t = setInterval(o, 1e3 / n.fps)
        }

        function f() {
            t && (clearInterval(t), t = null);
            n.onStop(i / n.len, u)
        }

        function o() {
            var t = n.len;
            n.onUpdate(i / t, u);
            i == t && f();
            ++i
        }
        return n = si(n, {
            fps: 50,
            len: 15,
            onUpdate: l,
            onStop: l
        }), r && (n.len = Math.round(n.len / 2)), e(), {
            start: e,
            stop: f,
            update: o,
            args: n,
            map: u
        }
    }

    function c(n, t) {
        return t === "" ? r ? n.style.filter = "" : n.style.opacity = "" : t != null ? r ? n.style.filter = "alpha(opacity=" + t * 100 + ")" : n.style.opacity = t : r ? /alpha\(opacity=([0-9.])+\)/.test(n.style.opacity) && (t = parseFloat(RegExp.$1) / 100) : t = parseFloat(n.style.opacity), t
    }

    function yt(n, t) {
        var i = n.style;
        return t != null && (i.display = t ? "" : "none"), i.display != "none"
    }

    function ci(n, t) {
        var i = r ? n.clientX + document.body.scrollLeft : n.pageX,
            u = r ? n.clientY + document.body.scrollTop : n.pageY;
        return t && (i -= t.x, u -= t.y), {
            x: i,
            y: u
        }
    }

    function ut(n) {
        var r = 0,
            u = 0,
            f = /^div$/i.test(n.tagName),
            t, i;
        return f && n.scrollLeft && (r = n.scrollLeft), f && n.scrollTop && (u = n.scrollTop), t = {
            x: n.offsetLeft - r,
            y: n.offsetTop - u
        }, n.offsetParent && (i = ut(n.offsetParent), t.x += i.x, t.y += i.y), t
    }

    function rr() {
        var n = document.documentElement,
            t = document.body;
        return {
            x: n.scrollLeft || t.scrollLeft,
            y: n.scrollTop || t.scrollTop,
            w: n.clientWidth || window.innerWidth || t.clientWidth,
            h: n.clientHeight || window.innerHeight || t.clientHeight
        }
    }

    function li(n, t, i) {
        for (i = 0; i < n.length; ++i) t(n[i])
    }

    function ft(n) {
        return typeof n == "string" && (n = document.getElementById(n)), n
    }
    var w = navigator.userAgent,
        pt = /opera/i.test(w),
        ai = /Konqueror|Safari|KHTML/i.test(w),
        r = /msie/i.test(w) && !pt && !/mac_powerpc/i.test(w),
        et = r && /msie 6/i.test(w),
        vi = /gecko/i.test(w) && !ai && !pt && !r,
        t = n.prototype,
        k = n.I18N = {},
        s, st, ii, ui, fi, a, l;
    return n.SEL_NONE = 0, n.SEL_SINGLE = 1, n.SEL_MULTIPLE = 2, n.SEL_WEEK = 3, n.dateToInt = o, n.intToDate = f, n.printDate = y, n.formatString = ei, n.i18n = u, n.LANG = function(n, t, i) {
        k.__ = k[n] = {
            name: t,
            data: i
        }
    }, n.setup = function(t) {
        return new n(t)
    }, t.moveTo = function(n, t) {
        n = nt(n);
        var r = g(n, this.date, !0),
            e, f = this.args,
            v = f.min && g(n, f.min),
            y = f.max && g(n, f.max);
        if (f.showTime && (this.setHours(n.getUTCHours()), this.setMinutes(n.getUTCMinutes())), f.animation || (t = !1), p(v != null && v <= 1, [this.els.navPrevMonth, this.els.navPrevYear], "DynarchCalendar-navDisabled"), p(y != null && y >= -1, [this.els.navNextMonth, this.els.navNextYear], "DynarchCalendar-navDisabled"), v < -1 && (n = f.min, e = 1, r = 0), y > 1 && (n = f.max, e = 2, r = 0), this.date = n, this.refresh(!!t), this.callHooks("onChange", this, n, t), t && !(r == 0 && t == 2)) {
            this._bodyAnim && this._bodyAnim.stop();
            var o = this.els.body,
                w = hi("div", "DynarchCalendar-animBody-" + ii[r], o),
                l = o.firstChild,
                ut = c(l) || .7,
                d = e ? a.brakes : r == 0 ? a.shake : a.accel_ab2,
                s = r * r > 4,
                tt = s ? l.offsetTop : l.offsetLeft,
                b = w.style,
                u = s ? o.offsetHeight : o.offsetWidth;
            if (r < 0 ? u += tt : r > 0 ? u = tt - u : (u = Math.round(u / 7), e == 2 && (u = -u)), !e && r != 0) {
                var h = w.cloneNode(!0),
                    k = h.style,
                    it = 2 * u;
                h.appendChild(l.cloneNode(!0));
                k[s ? "marginTop" : "marginLeft"] = u + "px";
                o.appendChild(h)
            }
            l.style.visibility = "hidden";
            w.innerHTML = ot(this);
            this._bodyAnim = rt({
                onUpdate: i(function(n, t) {
                    var i = d(n),
                        f;
                    h && (f = t(i, u, it) + "px");
                    e ? b[s ? "marginTop" : "marginLeft"] = t(i, u, 0) + "px" : ((s || r == 0) && (b.marginTop = t(r == 0 ? d(n * n) : i, 0, u) + "px", r != 0 && (k.marginTop = f)), s && r != 0 || (b.marginLeft = t(i, 0, u) + "px", r != 0 && (k.marginLeft = f)));
                    this.args.opacity > 2 && h && (c(h, 1 - i), c(w, i))
                }, this),
                onStop: i(function() {
                    o.innerHTML = ot(this, n);
                    this._bodyAnim = null
                }, this)
            })
        }
        return this._lastHoverDate = null, v >= -1 && y <= 1
    }, t.isDisabled = function(n) {
        var t = this.args;
        return t.min && g(n, t.min) < 0 || t.max && g(n, t.max) > 0 || t.disabled(n)
    }, t.toggleMenu = function() {
        v(this, !this._menuVisible)
    }, t.refresh = function(n) {
        var t = this.els;
        n || (t.body.innerHTML = ot(this));
        t.title.innerHTML = bt(this);
        t.yearInput.value = this.date.getFullYear()
    }, t.redraw = function() {
        var n = this.els;
        this.refresh();
        n.dayNames.innerHTML = wt(this);
        n.menu.innerHTML = kt(this);
        n.bottomBar && (n.bottomBar.innerHTML = dt(this));
        it(n.topCont, i(function(t) {
            var i = st[t.className];
            i && (n[i] = t);
            t.className == "DynarchCalendar-menu-year" ? (h(t, this._focusEvents), n.yearInput = t) : r && t.setAttribute("unselectable", "on")
        }, this));
        this.setTime(null, !0)
    }, t.setLanguage = function(t) {
        var i = n.setLanguage(t);
        i && (this.fdow = i.data.fdow, this.redraw())
    }, n.setLanguage = function(n) {
        var t = k[n];
        return t && (k.__ = t), t
    }, t.focus = function() {
        try {
            this.els[this._menuVisible ? "yearInput" : "focusLink"].focus()
        } catch (n) {}
        gt.call(this)
    }, t.blur = function() {
        this.els.focusLink.blur();
        this.els.yearInput.blur();
        ni.call(this)
    }, t.showAt = function(n, t, i) {
        this._showAnim && this._showAnim.stop();
        i = i && this.args.animation;
        var u = this.els.topCont,
            f = this,
            e = this.els.body.firstChild,
            o = e.offsetHeight,
            r = u.style;
        r.position = "absolute";
        r.left = n + "px";
        r.top = t + "px";
        r.zIndex = 1e4;
        r.display = "";
        i && (e.style.marginTop = -o + "px", this.args.opacity > 1 && c(u, 0), this._showAnim = rt({
            onUpdate: function(n, t) {
                e.style.marginTop = -t(a.accel_b(n), o, 0) + "px";
                f.args.opacity > 1 && c(u, n)
            },
            onStop: function() {
                f.args.opacity > 1 && c(u, "");
                f._showAnim = null
            }
        }))
    }, t.hide = function() {
        var n = this.els.topCont,
            t = this,
            i = this.els.body.firstChild,
            u = i.offsetHeight,
            r = ut(n).y;
        this.args.animation ? (this._showAnim && this._showAnim.stop(), this._showAnim = rt({
            onUpdate: function(f, e) {
                t.args.opacity > 1 && c(n, 1 - f);
                i.style.marginTop = -e(a.accel_b(f), 0, u) + "px";
                n.style.top = e(a.accel_ab(f), r, r - 10) + "px"
            },
            onStop: function() {
                n.style.display = "none";
                i.style.marginTop = "";
                t.args.opacity > 1 && c(n, "");
                t._showAnim = null
            }
        })) : n.style.display = "none";
        this.inputField = null
    }, t.popup = function(n, t) {
        function e(t) {
            var u = {
                x: i.x,
                y: i.y
            };
            return t ? (/B/.test(t) && (u.y += n.offsetHeight), /b/.test(t) && (u.y += n.offsetHeight - r.y), /T/.test(t) && (u.y -= r.y), /l/.test(t) && (u.x -= r.x - n.offsetWidth), /L/.test(t) && (u.x -= r.x), /R/.test(t) && (u.x += n.offsetWidth), /c/i.test(t) && (u.x += (n.offsetWidth - r.x) / 2), /m/i.test(t) && (u.y += (n.offsetHeight - r.y) / 2), u) : u
        }
        var i;
        n = ft(n);
        t || (t = this.args.align);
        t = t.split(/\x2f/);
        var f = ut(n),
            o = this.els.topCont,
            s = o.style,
            r, u = rr();
        s.visibility = "hidden";
        s.display = "";
        this.showAt(0, 0);
        document.body.appendChild(o);
        r = {
            x: o.offsetWidth,
            y: o.offsetHeight
        };
        i = f;
        i = e(t[0]);
        i.y < u.y && (i.y = f.y, i = e(t[1]));
        i.x + r.x > u.x + u.w && (i.x = f.x, i = e(t[2]));
        i.y + r.y > u.y + u.h && (i.y = f.y, i = e(t[3]));
        i.x < u.x && (i.x = f.x, i = e(t[4]));
        this.showAt(i.x, i.y, !0);
        s.visibility = "";
        this.focus()
    }, t.manageFields = function(t, r, u) {
        r = ft(r);
        h(ft(t), "click", i(function() {
            if (this.inputField = r, this.dateFormat = u, this.selection.type == n.SEL_SINGLE) {
                var i, f, e, o;
                i = /input|textarea/i.test(r.tagName) ? r.value : r.innerText || r.textContent;
                i && (f = /(^|[^%])%[bBmo]/.exec(u), e = /(^|[^%])%[de]/.exec(u), f && e && (o = f.index < e.index), i = Calendar.parseDate(i, o), i && (this.selection.set(i, !1, !0), this.moveTo(i)))
            }
            this.popup(t)
        }, this))
    }, t.callHooks = function(n) {
        for (var r = tt(arguments, 1), i = this.handlers[n], t = 0; t < i.length; ++t) i[t].apply(this, r)
    }, t.addEventListener = function(n, t) {
        this.handlers[n].push(t)
    }, t.removeEventListener = function(n, t) {
        for (var i = this.handlers[n], r = i.length; --r >= 0;) i[r] === t && i.splice(r, 1)
    }, t.getTime = function() {
        return this.time
    }, t.setTime = function(n, t) {
        if (this.args.showTime) {
            n = this.time = n != null ? n : this.time;
            var i = this.getHours(),
                r = this.getMinutes(),
                f = i < 12;
            this.args.showTime == 12 && (i == 0 && (i = 12), i > 12 && (i -= 12), this.els.timeAM.innerHTML = u(f ? "AM" : "PM"));
            i < 10 && (i = "0" + i);
            r < 10 && (r = "0" + r);
            this.els.timeHour.innerHTML = i;
            this.els.timeMinute.innerHTML = r;
            t || this.callHooks("onTimeChange", this, n)
        }
    }, t.getHours = function() {
        return Math.floor(this.time / 100)
    }, t.getMinutes = function() {
        return this.time % 100
    }, t.setHours = function(n) {
        n < 0 && (n += 24);
        this.setTime(100 * (n % 24) + this.time % 100)
    }, t.setMinutes = function(n) {
        n < 0 && (n += 60);
        this.setTime(100 * this.getHours() + n % 60)
    }, t._getInputYear = function() {
        var n = parseInt(this.els.yearInput.value, 10);
        return isNaN(n) && (n = this.date.getFullYear()), n
    }, t._showTooltip = function(n) {
        var i = "",
            t, r = this.els.tooltip;
        n && (n = f(n), t = this.args.dateInfo(n), t && t.tooltip && (i = "<div class='DynarchCalendar-tooltipCont'>" + y(n, t.tooltip) + "<\/div>"));
        r.innerHTML = i
    }, s = " align='center' cellspacing='0' cellpadding='0'", st = {
        "DynarchCalendar-topCont": "topCont",
        "DynarchCalendar-focusLink": "focusLink",
        DynarchCalendar: "main",
        "DynarchCalendar-topBar": "topBar",
        "DynarchCalendar-title": "title",
        "DynarchCalendar-dayNames": "dayNames",
        "DynarchCalendar-body": "body",
        "DynarchCalendar-menu": "menu",
        "DynarchCalendar-menu-year": "yearInput",
        "DynarchCalendar-bottomBar": "bottomBar",
        "DynarchCalendar-tooltip": "tooltip",
        "DynarchCalendar-time-hour": "timeHour",
        "DynarchCalendar-time-minute": "timeMinute",
        "DynarchCalendar-time-am": "timeAM",
        "DynarchCalendar-navBtn DynarchCalendar-prevYear": "navPrevYear",
        "DynarchCalendar-navBtn DynarchCalendar-nextYear": "navNextYear",
        "DynarchCalendar-navBtn DynarchCalendar-prevMonth": "navPrevMonth",
        "DynarchCalendar-navBtn DynarchCalendar-nextMonth": "navNextMonth"
    }, ii = {
        "-3": "backYear",
        "-2": "back",
        "0": "now",
        "2": "fwd",
        "3": "fwdYear"
    }, ui = {
        37: -1,
        38: -2,
        39: 1,
        40: 2
    }, fi = {
        33: -1,
        34: 1
    }, t._getDateDiv = function(n) {
        var t = null;
        if (n) try {
            it(this.els.body, function(i) {
                if (i.getAttribute("dyc-date") == n) throw t = i;
            })
        } catch (i) {}
        return t
    }, (n.Selection = function(n, t, r, u) {
        this.type = t;
        this.sel = n instanceof Array ? n : [n];
        this.onChange = i(r, u);
        this.cal = u
    }).prototype = {
        get: function() {
            return this.type == n.SEL_SINGLE ? this.sel[0] : this.sel
        },
        isEmpty: function() {
            return this.sel.length == 0
        },
        set: function(t, i, r) {
            var u = this.type == n.SEL_SINGLE;
            if (t instanceof Array) {
                if (this.sel = t, this.normalize(), !r) this.onChange(this)
            } else if (t = o(t), u || !this.isSelected(t)) {
                if (u ? this.sel = [t] : this.sel.splice(this.findInsertPos(t), 0, t), this.normalize(), !r) this.onChange(this)
            } else i && this.unselect(t, r)
        },
        reset: function() {
            this.sel = [];
            this.set.apply(this, arguments)
        },
        countDays: function() {
            for (var t = 0, i = this.sel, r = i.length, n, u, e; --r >= 0;) n = i[r], n instanceof Array && (u = f(n[0]), e = f(n[1]), t += Math.round(Math.abs(e.getTime() - u.getTime()) / 864e5)), ++t;
            return t
        },
        unselect: function(n, t) {
            var u, e, s, i, r, h, c;
            for (n = o(n), u = !1, e = this.sel, s = e.length; --s >= 0;) i = e[s], i instanceof Array ? n >= i[0] && n <= i[1] && (r = f(n), h = r.getDate(), n == i[0] ? (r.setDate(h + 1), i[0] = o(r), u = !0) : n == i[1] ? (r.setDate(h - 1), i[1] = o(r), u = !0) : (c = new Date(r), c.setDate(h + 1), r.setDate(h - 1), e.splice(s + 1, 0, [o(c), i[1]]), i[1] = o(r), u = !0)) : n == i && (e.splice(s, 1), u = !0);
            if (u && (this.normalize(), !t)) this.onChange(this)
        },
        normalize: function() {
            var t, i, n, e, s, r, u;
            for (this.sel = this.sel.sort(function(n, t) {
                    return n instanceof Array && (n = n[0]), t instanceof Array && (t = t[0]), n - t
                }), t = this.sel, i = t.length; --i >= 0;) {
                if (n = t[i], n instanceof Array) {
                    if (n[0] > n[1]) {
                        t.splice(i, 1);
                        continue
                    }
                    n[0] == n[1] && (n = t[i] = n[0])
                }
                e && (s = e, r = n instanceof Array ? n[1] : n, r = f(r), r.setDate(r.getDate() + 1), r = o(r), r >= s && (u = t[i + 1], n instanceof Array && u instanceof Array ? (n[1] = u[1], t.splice(i + 1, 1)) : n instanceof Array ? (n[1] = e, t.splice(i + 1, 1)) : u instanceof Array ? (u[0] = n, t.splice(i, 1)) : (t[i] = [n, u], t.splice(i + 1, 1))));
                e = n instanceof Array ? n[0] : n
            }
        },
        findInsertPos: function(n) {
            for (var r = this.sel, i = r.length, t; --i >= 0;)
                if (t = r[i], t instanceof Array && (t = t[0]), t <= n) break;
            return i + 1
        },
        clear: function(n) {
            if (this.sel = [], !n) this.onChange(this)
        },
        selectRange: function(t, r) {
            var f, u;
            if (t = o(t), r = o(r), t > r && (f = t, t = r, r = f), u = this.cal.args.checkRange, !u) return this._do_selectRange(t, r);
            try {
                li(new n.Selection([
                    [t, r]
                ], n.SEL_MULTIPLE, l).getDates(), i(function(n) {
                    if (this.isDisabled(n)) {
                        u instanceof Function && u(n, this);
                        throw "OUT";
                    }
                }, this.cal));
                this._do_selectRange(t, r)
            } catch (e) {}
        },
        _do_selectRange: function(n, t) {
            this.sel.push([n, t]);
            this.normalize();
            this.onChange(this)
        },
        isSelected: function(n) {
            for (var i = this.sel.length, t; --i >= 0;)
                if (t = this.sel[i], t instanceof Array && n >= t[0] && n <= t[1] || n == t) return !0;
            return !1
        },
        getFirstDate: function() {
            var n = this.sel[0];
            return n && n instanceof Array && (n = n[0]), n
        },
        getLastDate: function() {
            if (this.sel.length > 0) {
                var n = this.sel[this.sel.length - 1];
                return n && n instanceof Array && (n = n[1]), n
            }
        },
        print: function(n, t) {
            var r = [],
                o = 0,
                i, u = this.cal.getHours(),
                e = this.cal.getMinutes();
            for (t || (t = " -> "); o < this.sel.length;) i = this.sel[o++], i instanceof Array ? r.push(y(f(i[0], u, e), n) + t + y(f(i[1], u, e), n)) : r.push(y(f(i, u, e), n));
            return r
        },
        getDates: function(n) {
            for (var r = [], u = 0, t, i; u < this.sel.length;) {
                if (i = this.sel[u++], i instanceof Array)
                    for (t = f(i[0]), i = i[1]; o(t) < i;) r.push(n ? y(t, n) : new Date(t)), t.setDate(t.getDate() + 1);
                else t = f(i);
                r.push(n ? y(t, n) : t)
            }
            return r
        }
    }, n.parseDate = function(n, t, i) {
        var l, f, c, r;
        if (!/\S/.test(n)) return "";
        n = n.replace(/^\s+/, "").replace(/\s+$/, "");
        i = i || new Date;
        var o = null,
            u = null,
            s = null,
            h = null,
            a = null,
            v = null,
            e = n.match(/([0-9]{1,2}):([0-9]{1,2})(:[0-9]{1,2})?\s*(am|pm)?/i);
        for (e && (h = parseInt(e[1], 10), a = parseInt(e[2], 10), v = e[3] ? parseInt(e[3].substr(1), 10) : 0, n = n.substring(0, e.index) + n.substr(e.index + e[0].length), e[4] && (e[4].toLowerCase() == "pm" && h < 12 ? h += 12 : e[4].toLowerCase() == "am" && h >= 12 && (h -= 12))), l = n.split(/\W+/), f = [], c = 0; c < l.length; ++c) r = l[c], /^[0-9]{4}$/.test(r) ? (o = parseInt(r, 10), u || s || t != null || (t = !0)) : /^[0-9]{1,2}$/.test(r) ? (r = parseInt(r, 10), r >= 60 ? o = r : r >= 0 && r <= 12 ? f.push(r) : r >= 1 && r <= 31 && (s = r)) : u = ir(r);
        return f.length >= 2 ? t ? (u || (u = f.shift()), s || (s = f.shift())) : (s || (s = f.shift()), u || (u = f.shift())) : f.length == 1 && (s ? u || (u = f.shift()) : s = f.shift()), o || (o = f.length > 0 ? f.shift() : i.getFullYear()), o < 30 ? o += 2e3 : o < 99 && (o += 1900), u || (u = i.getMonth() + 1), o && u && s ? new Date(Date.UTC(o, u - 1, s, h, a, v)) : null
    }, a = {
        elastic_b: function(n) {
            return 1 - Math.cos(-n * 5.5 * Math.PI) / Math.pow(2, 7 * n)
        },
        magnetic: function(n) {
            return 1 - Math.cos(n * n * n * 10.5 * Math.PI) / Math.exp(4 * n)
        },
        accel_b: function(n) {
            return n = 1 - n, 1 - n * n * n * n
        },
        accel_a: function(n) {
            return n * n * n
        },
        accel_ab: function(n) {
            return n = 1 - n, 1 - Math.sin(n * n * Math.PI / 2)
        },
        accel_ab2: function(n) {
            return (n /= .5) < 1 ? 1 / 2 * n * n : -1 / 2 * (--n * (n - 2) - 1)
        },
        brakes: function(n) {
            return n = 1 - n, 1 - Math.sin(n * n * Math.PI)
        },
        shake: function(n) {
            return n < .5 ? -Math.cos(n * 11 * Math.PI) * n * n : (n = 1 - n, Math.cos(n * 11 * Math.PI) * n * n)
        }
    }, l = new Function, n
}();
Calendar.LANG("en", "English", {
    fdow: 1,
    goToday: "Go Today",
    today: "Today",
    wk: "wk",
    weekend: "0,6",
    AM: "am",
    PM: "pm",
    mn: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
    smn: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
    dn: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"],
    sdn: ["Su", "Mo", "Tu", "We", "Th", "Fr", "Sa", "Su"]
});
Array.prototype.indexOf = function(n) {
    for (var t = 0; t < this.length; t++)
        if (this[t] == n) return t;
    return -1
};
Array.prototype.filter = function(n) {
    for (var i = [], t = 0; t < this.length; t++) n(this[t]) && (i[i.length] = this[t]);
    return i
};
String.prototype.right = function(n) {
    return n >= this.length ? this : this.substr(this.length - n, n)
};
String.prototype.left = function(n) {
    return n >= this.length ? this : this.substr(0, n)
};
String.prototype.trim = function() {
    return this.replace(/^\s+|\s+$/, "")
};
monthNames = "January February March April May June July August September October November December".split(" ");
dateParsePatterns = [{
    re: /(\d{1,2}) (\d{1,2}) (\d{4})/,
    handler: function(n) {
        var t = parseInt(n[3], 10),
            i = parseInt(n[1], 10),
            r = parseInt(n[2], 10) - 1;
        if (DateInRange(t, r, i)) return new Date(t, r, i)
    }
}, {
    re: /(\d{1,2}) (\d{1,2}) (\d{1,2})/,
    handler: function(n) {
        var t = parseInt(n[3], 10),
            i, r;
        return t = t < 80 ? t + 2e3 : t + 1900, i = parseInt(n[1], 10), r = parseInt(n[2], 10) - 1, DateInRange(t, r, i) ? new Date(t, r, i) : void 0
    }
}, {
    re: /(\d{1,2}) (\d{1,2})/,
    handler: function(n) {
        var u = new Date,
            t = u.getFullYear(),
            i = parseInt(n[1], 10),
            r = parseInt(n[2], 10) - 1;
        if (DateInRange(t, r, i)) return new Date(t, r, i)
    }
}, {
    re: /^(\d{1,2})$/i,
    handler: function(n) {
        var t = new Date,
            i = t.getFullYear(),
            r = parseInt(n[1], 10),
            u = t.getMonth();
        if (DateInRange(i, u, r)) return new Date(i, u, r)
    }
}, {
    re: /^(\d{1,2}) (\w+)$/i,
    handler: function(n) {
        var u = new Date,
            t = u.getFullYear(),
            i = parseInt(n[1], 10),
            r = parseMonth(n[2]);
        if (DateInRange(t, r, i)) return new Date(t, r, i)
    }
}, {
    re: /^(\d{1,2}) (\w+),? (\d{4})$/i,
    handler: function(n) {
        var t = parseInt(n[3], 10),
            i = parseInt(n[1], 10),
            r = parseMonth(n[2]);
        if (DateInRange(t, r, i)) return new Date(t, r, i)
    }
}, {
    re: /^(\d{1,2}) (\w+),? (\d{1,2})/,
    handler: function(n) {
        var t = parseInt(n[3], 10),
            i, r;
        return t = t < 80 ? t + 2e3 : t + 1900, i = parseInt(n[1], 10), r = parseMonth(n[2]), DateInRange(t, r, i) ? new Date(t, r, i) : void 0
    }
}];