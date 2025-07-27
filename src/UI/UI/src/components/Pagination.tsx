// components/Pagination.tsx
type PaginationProps = {
  page: number;
  totalPages: number;
  onPageChange: (page: number) => void;
};

export default function Pagination({
  page,
  totalPages,
  onPageChange,
}: PaginationProps) {
  if (totalPages <= 1) return null; // no pagination needed

  return (
    <div className="flex justify-center mt-6">
      <div className="join">
        <button
          className="join-item btn"
          disabled={page === 1}
          onClick={() => onPageChange(page - 1)}
        >
          «
        </button>

        {Array.from({ length: totalPages }).map((_, idx) => {
          const pageNum = idx + 1;
          return (
            <button
              key={pageNum}
              className={`join-item btn ${
                page === pageNum ? "btn-active" : ""
              }`}
              onClick={() => onPageChange(pageNum)}
            >
              {pageNum}
            </button>
          );
        })}

        <button
          className="join-item btn"
          disabled={page === totalPages}
          onClick={() => onPageChange(page + 1)}
        >
          »
        </button>
      </div>
    </div>
  );
}
