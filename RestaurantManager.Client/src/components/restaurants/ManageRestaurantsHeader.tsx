import { Plus } from "lucide-react";

type ManageRestaurantsHeaderProps = {
    subtitle: string;
    setCreateOpen: (v: boolean) => void;
};

export default ({
    subtitle,
    setCreateOpen
}: ManageRestaurantsHeaderProps) => {
    return (
        <div className="flex flex-col gap-3 md:flex-row md:items-center md:justify-between">
            <div>
                <p className="mt-1 text-sm text-slate-600">{subtitle}</p>
            </div>

            <button
                onClick={() => setCreateOpen(true)}
                className="inline-flex items-center justify-center gap-2 rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white hover:bg-blue-700"
            >
                <Plus className="h-4 w-4" />
                New Restaurant
            </button>
        </div>
    )
};