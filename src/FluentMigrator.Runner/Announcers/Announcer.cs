﻿#region License

// Copyright (c) 2007-2009, Sean Chambers <schambers80@gmail.com>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;

namespace FluentMigrator.Runner.Announcers
{
    public abstract class Announcer : IAnnouncer
    {
        public virtual bool ShowSql { get; set; }
        public virtual bool ShowElapsedTime { get; set; }

        private long? currentMigrationVersion = null;

        public virtual void StartMigration(long version)
        {
            if (currentMigrationVersion.HasValue && currentMigrationVersion.Value != version)
                throw new InvalidOperationException("Attempt to start migration when another migration was already in process");
            currentMigrationVersion = version;
        }

        public virtual void EndMigration()
        {
            if (!currentMigrationVersion.HasValue)
                throw new InvalidOperationException("Attempt to end migration when no migration was in process");
            currentMigrationVersion = null;
        }

        public virtual void Heading(string message)
        {
            Write(message, true);
        }

        public virtual void Say(string message)
        {
            Write(message, true);
        }

        public virtual void SayTime(string message)
        {
            Say(message);
        }

        public virtual void Emphasize(string message)
        {
            Write(message, true);
        }

        public virtual void Sql(string sql)
        {
            if (!ShowSql) return;

            if (string.IsNullOrEmpty(sql))
                Write("No SQL statement executed.", true);
            else Write(sql, false);
        }

        public virtual void ElapsedTime(TimeSpan timeSpan)
        {
            if (!ShowElapsedTime) return;

            Write(string.Format("=> {0}s", timeSpan.TotalSeconds), true);
        }

        public virtual void Error(string message)
        {
            Write(string.Format("!!! {0}", message), true);
        }

        public abstract void Write(string message, bool escaped);
    }
}