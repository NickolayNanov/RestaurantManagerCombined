import { Plus, ArrowRight } from "lucide-react";
import FragmentCategoryGroup from "./FragmentCategoryGroup";
import type { MenuItem } from "../../types/menu-item-types";
import type { MenuWithItems } from "../../types/menu-types";
import { useMemo } from "react";
import { normalizeCategory } from "../helper";

type MenuItemsSectionProps = {
    setItemCreateOpen: (v: boolean) => void;
    setItemDeleteTarget: (v: MenuItem) => void;
    setItemEditTarget: (v: MenuItem) => void;
    menu: MenuWithItems;
}

const MenuItemsSection = ({
    menu,
    setItemCreateOpen,
    setItemDeleteTarget,
    setItemEditTarget
}: MenuItemsSectionProps) => {

    // ✅ Dynamic grouping by DISTINCT categories from all items
    const grouped = useMemo(() => {
        const map = new Map<string, MenuItem[]>();
        for (const it of menu.items) {
            const cat = normalizeCategory(it.categoryText);
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

    return (
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
    )
};

export default MenuItemsSection;