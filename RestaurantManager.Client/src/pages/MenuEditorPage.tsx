import { useEffect, useMemo, useRef, useState } from "react";
import { Link, useParams } from "react-router-dom";
import {
  ArrowLeft,
  ArrowRight,
  BadgeCheck,
  Copy,
  Image as ImageIcon,
  Pencil,
  Plus,
  RefreshCw,
  Tag,
  Trash2,
  UtensilsCrossed,
} from "lucide-react";
import type { MenuItem } from "../types/menu-item-types";
import type {  MenuWithItems } from "../types/menu-types";
import ModalShell from "../components/modals/ModalShell";
import FragmentCategoryGroup from "../components/menu-items/FragmentCategoryGroup";
import MenuItemForm from "../components/menu-items/MenuItemForm";
import { seedMenu } from "../data/dashboard";
import MenuEditForm from "../components/menus/MenuEditForm";
import Pill from "../components/shared/Pill";
import { normalizeCategory } from "../components/helper";
import { apiFetch } from "../api/apiFetch";

// ---------- forms ----------
export type MenuEditValues = Pick<MenuWithItems, "name" | "description" | "imgUrl" | "isActive" | "type">;
export type MenuItemFormValues = Pick<MenuItem, "name" | "price" | "imgUrl" | "isActive" | "category">;

const emptyItem: MenuItemFormValues = {
  name: "",
  price: 0,
  imgUrl: "",
  isActive: true,
  category: "",
};

// ---------- page ----------
const MenuEditorPage = () => {
  const { restaurantId = "r1", menuId = "m1" } = useParams();

  const [menu, setMenu] = useState<MenuWithItems>(() => seedMenu(restaurantId, menuId));

  const [menuEditOpen, setMenuEditOpen] = useState(false);

  const [itemCreateOpen, setItemCreateOpen] = useState(false);
  const [itemEditTarget, setItemEditTarget] = useState<MenuItem | null>(null);
  const [itemDeleteTarget, setItemDeleteTarget] = useState<MenuItem | null>(null);

  // ✅ Dynamic grouping by DISTINCT categories from all items
  const grouped = useMemo(() => {
    const map = new Map<string, MenuItem[]>();
    debugger
    for (const it of menu.items) {
      const cat = normalizeCategory(it.category);
      if (!map.has(cat)) map.set(cat, []);
      map.get(cat)!.push(it);
    }

    // Sort categories alphabetically
    return Array.from(map.entries())
      .sort(([a], [b]) => a.localeCompare(b))
      .map(([category, items]) => ({
        category,
        items: items.slice().sort((x, y) => x.name.localeCompare(y.name)),
      }));
  }, [menu.items]);

  const didInit = useRef(false);
  
      useEffect(() => {
          if (didInit.current) return;
          didInit.current = true;
  
          void fetchMenuData();
      }, []);

  const fetchMenuData = async () => {
    const menuData: MenuWithItems = await apiFetch(`api/menus/${menuId}`, {
      method: "GET"
    })

    if (menuData) {
      setMenu(menuData);
    }
  };

  const editMenu = (v: MenuEditValues) => {
    setMenu((p) => ({ ...p, ...v }));
    setMenuEditOpen(false);
  };

  const addItem = (v: MenuItemFormValues) => {
    const newItem: MenuItem = {
      id: crypto.randomUUID(),
      ...v,
      category: normalizeCategory(v.category),
    };

    setMenu((p) => ({ ...p, items: [newItem, ...p.items] }));
    setItemCreateOpen(false);
  };

  const updateItem = (id: string, v: MenuItemFormValues) => {
    setMenu((p) => ({
      ...p,
      items: p.items.map((it) => (it.id === id ? { ...it, ...v, category: normalizeCategory(v.category) } : it)),
    }));
    setItemEditTarget(null);
  };

  const deleteItem = (id: string) => {
    setMenu((p) => ({ ...p, items: p.items.filter((it) => it.id !== id) }));
    setItemDeleteTarget(null);
  };

  const copyId = async (value: string|null) => {
    try {
      await navigator.clipboard.writeText(value ?? "");
    } catch {
    }
  };

  return (
    <div className="space-y-4">
      {/* Breadcrumb + header */}
      <div className="flex flex-col gap-2 md:flex-row md:items-center md:justify-between">
        <div>
          <div className="flex items-center gap-2 text-sm text-slate-600">
            <Link to="/manage-restaurants" className="inline-flex items-center gap-2 hover:text-slate-900">
              <ArrowLeft className="h-4 w-4" />
              Restaurants
            </Link>
            <span className="text-slate-400">/</span>
            <Link to={`/manage-restaurants/${restaurantId}`} className="hover:text-slate-900">
              Restaurant
            </Link>
            <span className="text-slate-400">/</span>
            <span className="font-medium text-slate-900">{menu.name}</span>
          </div>

          <h2 className="mt-2 text-xl font-semibold text-slate-900">{menu.name}</h2>
        </div>

        <div className="flex items-center gap-2">
          <button
            onClick={() => setMenuEditOpen(true)}
            className="inline-flex items-center gap-2 rounded-lg border border-slate-200 bg-white px-3 py-2 text-sm font-semibold text-slate-700 hover:bg-slate-50"
          >
            <Pencil className="h-4 w-4" />
            Edit
          </button>

          <button
            onClick={() => alert("TODO: delete menu")}
            className="inline-flex items-center gap-2 rounded-lg border border-rose-200 bg-rose-50 px-3 py-2 text-sm font-semibold text-rose-700 hover:bg-rose-100"
          >
            <Trash2 className="h-4 w-4" />
            Delete
          </button>

          <button
            onClick={fetchMenuData}
            className="inline-flex items-center gap-2 rounded-lg border border-slate-200 bg-white px-3 py-2 text-sm font-semibold text-slate-700 hover:bg-slate-50"
            title="Refresh dummy data"
          >
            <RefreshCw className="h-4 w-4" />
          </button>
        </div>
      </div>

      {/* Menu info card */}
      <section className="rounded-xl border border-slate-200 bg-white p-5 shadow-sm">
        <div className="flex flex-col gap-4 md:flex-row md:items-start md:justify-between">
          <div className="flex items-start gap-4">
            <div className="h-28 w-40 overflow-hidden rounded-xl bg-slate-100">
              {menu.imgUrl ? (
                <img src={menu.imgUrl} alt={menu.name} className="h-full w-full object-cover" />
              ) : (
                <div className="flex h-full w-full items-center justify-center text-slate-500">
                  <ImageIcon className="h-6 w-6" />
                </div>
              )}
            </div>

            <div>
              <div className="text-lg font-semibold text-slate-900">{menu.name}</div>

              <div className="mt-2 flex flex-wrap items-center gap-2">
                <Pill tone="blue">
                  <Tag className="mr-2 h-3.5 w-3.5" />
                  {menu.type}
                </Pill>

                <Pill tone={menu.isActive ? "green" : "slate"}>
                  <BadgeCheck className="mr-2 h-3.5 w-3.5" />
                  {menu.isActive ? "Active" : "Inactive"}
                </Pill>

                <Pill tone="slate">
                  <UtensilsCrossed className="mr-2 h-3.5 w-3.5" />
                  {menu.items.length} item(s)
                </Pill>
              </div>
            </div>
          </div>

          <div className="text-xs text-slate-500">
            Menu ID: <span className="font-semibold text-slate-700">{menu.id}</span>
            <button
              onClick={() => copyId(menu.id)}
              className="ml-2 inline-flex items-center rounded-md p-1 hover:bg-slate-100"
              title="Copy"
            >
              <Copy className="h-3.5 w-3.5 text-slate-600" />
            </button>
          </div>
        </div>

        <div className="mt-4 border-t border-slate-200 pt-4 text-sm text-slate-700">{menu.description}</div>
      </section>

      {/* Menu Items (grouped by category) */}
      <section className="rounded-xl border border-slate-200 bg-white shadow-sm">
        <div className="flex items-center justify-between border-b border-slate-200 px-5 py-4">
          <div>
            <h3 className="text-sm font-semibold text-slate-900">Menu Items</h3>
            <p className="mt-1 text-sm text-slate-600">Manage the items linked to this menu. Items are grouped by category.</p>
          </div>

          <button
            onClick={() => setItemCreateOpen(true)}
            className="inline-flex items-center gap-2 rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white hover:bg-blue-700"
          >
            <Plus className="h-4 w-4" />
            Add Item
            <ArrowRight className="h-4 w-4" />
          </button>
        </div>

        <div className="overflow-x-auto px-5 py-4">
          <table className="min-w-full table-auto">
            <thead className="bg-slate-50">
              <tr className="text-left text-xs font-semibold text-slate-600">
                <th className="px-4 py-3">Name</th>
                <th className="px-4 py-3">Price</th>
                <th className="px-4 py-3">Category</th>
                <th className="px-4 py-3 text-right">Actions</th>
              </tr>
            </thead>

            <tbody className="divide-y divide-slate-100">
              {grouped.map((g) => (
                <FragmentCategoryGroup
                  key={g.category}
                  category={g.category}
                  items={g.items}
                  onEdit={(it) => setItemEditTarget(it)}
                  onDelete={(it) => setItemDeleteTarget(it)}
                />
              ))}

              {menu.items.length === 0 && (
                <tr>
                  <td colSpan={4} className="px-4 py-14 text-center">
                    <div className="text-sm font-medium text-slate-900">No items yet</div>
                    <div className="mt-1 text-sm text-slate-600">
                      Click <span className="font-semibold">Add Item</span> to create the first menu item.
                    </div>
                  </td>
                </tr>
              )}
            </tbody>
          </table>

          <div className="mt-3 text-xs text-slate-600">Showing {menu.items.length} item(s)</div>
        </div>
      </section>

      {/* Modals */}
      {menuEditOpen && (
        <ModalShell title="Edit Menu" onClose={() => setMenuEditOpen(false)}>
          <MenuEditForm
            initial={{
              id: null,
              name: menu.name,
              description: menu.description,
              imgUrl: menu.imgUrl ?? "",
              isActive: menu.isActive,
              type: menu.type,
              restaurantId: menu.restaurantId
            }}
            onCancel={() => setMenuEditOpen(false)}
            onSubmit={editMenu}
          />
        </ModalShell>
      )}

      {itemCreateOpen && (
        <ModalShell title="Add Menu Item" onClose={() => setItemCreateOpen(false)}>
          <MenuItemForm initial={emptyItem} submitLabel="Create" onCancel={() => setItemCreateOpen(false)} onSubmit={addItem} />
        </ModalShell>
      )}

      {itemEditTarget && (
        <ModalShell title={`Edit: ${itemEditTarget.name}`} onClose={() => setItemEditTarget(null)}>
          <MenuItemForm
            initial={{
              name: itemEditTarget.name,
              price: itemEditTarget.price,
              imgUrl: itemEditTarget.imgUrl ?? "",
              isActive: itemEditTarget.isActive,
              category: itemEditTarget.category,
            }}
            submitLabel="Save"
            onCancel={() => setItemEditTarget(null)}
            onSubmit={(v) => updateItem(itemEditTarget.id, v)}
          />
        </ModalShell>
      )}

      {itemDeleteTarget && (
        <ModalShell title="Delete item?" onClose={() => setItemDeleteTarget(null)}>
          <div className="space-y-4">
            <p className="text-sm text-slate-700">
              Are you sure you want to delete <span className="font-semibold">{itemDeleteTarget.name}</span>?
            </p>

            <div className="flex justify-end gap-2">
              <button
                onClick={() => setItemDeleteTarget(null)}
                className="rounded-lg border border-slate-200 px-4 py-2 text-sm font-semibold text-slate-700 hover:bg-slate-50"
              >
                Cancel
              </button>
              <button
                onClick={() => deleteItem(itemDeleteTarget.id)}
                className="rounded-lg bg-rose-600 px-4 py-2 text-sm font-semibold text-white hover:bg-rose-700"
              >
                Delete
              </button>
            </div>
          </div>
        </ModalShell>
      )}
    </div>
  );
};

export default MenuEditorPage;