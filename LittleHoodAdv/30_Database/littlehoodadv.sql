-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- ホスト: 127.0.0.1
-- 生成日時: 2026-01-11 09:28:31
-- サーバのバージョン： 10.4.32-MariaDB
-- PHP のバージョン: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- データベース: `littlehoodadv`
--

-- --------------------------------------------------------

--
-- テーブルの構造 `save_data`
--

CREATE TABLE `save_data` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `stage_type` varchar(50) NOT NULL,
  `clear_time_h` int(11) NOT NULL,
  `clear_time_m` int(11) NOT NULL,
  `clear_time_s` int(11) NOT NULL,
  `clear_time_original` varchar(50) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- テーブルのデータのダンプ `save_data`
--

INSERT INTO `save_data` (`id`, `user_id`, `stage_type`, `clear_time_h`, `clear_time_m`, `clear_time_s`, `clear_time_original`, `created_at`) VALUES
(1, 1, 'stage1', 0, 16, 2, '16.461846', '2026-01-09 07:05:46'),
(2, 1, 'stage1', 0, 16, 2, '16.461846', '2026-01-09 07:06:49'),
(3, 2, 'tutorial', 0, 6, 34, '6.3413', '2026-01-09 07:16:22'),
(4, 2, 'stage1', 0, 20, 50, '20.30825', '2026-01-09 11:46:04'),
(5, 2, 'stage2', 0, 37, 32, '37.52183', '2026-01-09 13:01:18'),
(6, 2, 'stage1', 0, 31, 15, '31.35731', '2026-01-11 05:53:00'),
(7, 2, 'tutorial', 0, 7, 22, '7.427232', '2026-01-11 05:57:17'),
(8, 2, 'stage1', 0, 29, 20, '29.00345', '2026-01-11 06:14:06'),
(9, 2, 'stage2', 0, 47, 58, '47.98202', '2026-01-11 06:20:14'),
(10, 2, 'stage3', 0, 41, 52, '41.32854', '2026-01-11 06:34:21'),
(11, 2, 'stage3', 0, 37, 19, '37.99879', '2026-01-11 07:13:21'),
(12, 2, 'stage3', 0, 37, 7, '37.27861', '2026-01-11 07:15:16'),
(13, 5, 'tutorial', 0, 9, 33, '9.331245', '2026-01-11 07:30:47'),
(14, 6, 'tutorial', 0, 5, 44, '5.24126', '2026-01-11 07:33:59'),
(15, 6, 'stage1', 0, 26, 31, '26.11729', '2026-01-11 07:34:42'),
(16, 6, 'stage2', 0, 57, 6, '57.06952', '2026-01-11 07:36:41'),
(17, 2, 'stage3', 0, 40, 54, '40.74558', '2026-01-11 07:50:05'),
(18, 8, 'tutorial', 0, 7, 49, '7.095394', '2026-01-11 08:16:10'),
(19, 8, 'stage1', 0, 25, 35, '25.55373', '2026-01-11 08:17:20'),
(20, 9, 'tutorial', 0, 4, 19, '4.39902', '2026-01-11 08:19:45'),
(21, 9, 'stage1', 0, 25, 19, '25.39623', '2026-01-11 08:20:43'),
(22, 9, 'stage2', 0, 54, 35, '54.9548', '2026-01-11 08:21:49'),
(23, 9, 'stage3', 0, 37, 31, '37.51512', '2026-01-11 08:22:58');

-- --------------------------------------------------------

--
-- テーブルの構造 `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `user_name` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- テーブルのデータのダンプ `users`
--

INSERT INTO `users` (`user_id`, `user_name`) VALUES
(1, 'test'),
(2, 'test​'),
(3, 'test1​'),
(4, 'test2​'),
(5, 'testtest​'),
(6, 'unity​'),
(7, '​'),
(8, 'player1​'),
(9, 'player2​');

--
-- ダンプしたテーブルのインデックス
--

--
-- テーブルのインデックス `save_data`
--
ALTER TABLE `save_data`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_save_data_user` (`user_id`);

--
-- テーブルのインデックス `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`);

--
-- ダンプしたテーブルの AUTO_INCREMENT
--

--
-- テーブルの AUTO_INCREMENT `save_data`
--
ALTER TABLE `save_data`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- テーブルの AUTO_INCREMENT `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- ダンプしたテーブルの制約
--

--
-- テーブルの制約 `save_data`
--
ALTER TABLE `save_data`
  ADD CONSTRAINT `fk_save_data_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
